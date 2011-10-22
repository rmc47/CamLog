using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;
using System.IO;

namespace Engine
{
    public class ContactStore : IDisposable
    {
        private MySqlConnection m_Connection;
        private int m_SourceId;
        private CallsignLookup m_CallsignLookup = new CallsignLookup("cty.xml");

        public static ContactStore Create(string server, string database, string username, string password)
        {
            MySqlConnectionStringBuilder csb = new MySqlConnectionStringBuilder();
            csb.Server = server;
            //csb.Database = database;
            csb.UserID = username;
            csb.Password = password;

            try
            {
                using (MySqlConnection dbSetupConnection = new MySqlConnection(csb.ConnectionString))
                {
                    dbSetupConnection.Open();

                    // Check whether the DB already exists
                    using (MySqlCommand cmd = dbSetupConnection.CreateCommand())
                    {
                        cmd.CommandText = "SELECT schema_name FROM information_schema.schemata WHERE schema_name=?dbName;";
                        cmd.Parameters.AddWithValue("?dbName", database);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                            if (reader.Read())
                                throw new DatabaseAlreadyExistsException();
                    }

                    using (MySqlCommand cmd = dbSetupConnection.CreateCommand())
                    {
                        // TODO: Should escape / sanitise / something the DB parameter. Can't use a parameterised query here unfortunately.
                        cmd.CommandText = string.Format("CREATE DATABASE {0};", database);
                        cmd.ExecuteNonQuery();
                    }
                }
                csb.Database = database;
                using (MySqlConnection schemaSetupConnection = new MySqlConnection(csb.ConnectionString))
                {
                    schemaSetupConnection.Open();
                    using (MySqlCommand cmd = schemaSetupConnection.CreateCommand())
                    {
                        using (StreamReader reader = new StreamReader(System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("Engine.DBSchema.sql")))
                        {
                            string dbSchema = reader.ReadToEnd();
                            cmd.CommandText = dbSchema;
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new CannotCreateDatabaseException(ex.Message, ex.InnerException);
            }
            return new ContactStore(server, database, username, password);
        }

        public sealed class CannotCreateDatabaseException : Exception
        {
            public CannotCreateDatabaseException(string message, Exception innerException) : base(message, innerException)
            {
            }
        }
        public sealed class DatabaseAlreadyExistsException : Exception { }
        public sealed class DatabaseNotFoundException : Exception { }

        public ContactStore(string server, string database, string username, string password)
        {
            MySqlConnectionStringBuilder csb = new MySqlConnectionStringBuilder();
            csb.Server = server;
            csb.UserID = username;
            csb.Password = password;

            // Log in using the default DB, and check if the store exists
            using (MySqlConnection testConnection = new MySqlConnection(csb.ConnectionString))
            {
                testConnection.Open();
                using (MySqlCommand cmd = testConnection.CreateCommand())
                {
                    cmd.CommandText = "SELECT schema_name FROM information_schema.schemata WHERE schema_name=?dbName;";
                    cmd.Parameters.AddWithValue("?dbName", database);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                        if (!reader.Read())
                            throw new DatabaseNotFoundException();
                }
            }

            // Now we know it exists, do something sensible with it!
            csb.Database = database;
            m_Connection = new MySqlConnection(csb.ConnectionString);
            m_Connection.Open();
            
            using (MySqlCommand cmd = m_Connection.CreateCommand())
            {
                cmd.CommandText = "SELECT val FROM setup WHERE `key`='sourceId';";
                object sourceId = cmd.ExecuteScalar();
                if (!int.TryParse(sourceId as string, out m_SourceId))
                {
                    m_SourceId = new Random().Next();
                    cmd.CommandText = "INSERT INTO setup (`key`, val) VALUES ('sourceId', " + m_SourceId + ");";
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public int SourceId
        {
            get { return m_SourceId; }
        }

        public Contact LoadContact(int sourceId, int id)
        {
            lock (m_Connection)
            {
                using (MySqlCommand cmd = m_Connection.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM log WHERE id=?id;";
                    cmd.Parameters.AddWithValue("?id", id);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (!reader.Read())
                            return null;

                        Contact c = new Contact();
                        c.Id = id;
                        c.SourceId = (int)(long)reader["sourceId"];
                        c.LastModified = (DateTime)reader["lastModified"];
                        c.StartTime = (DateTime)reader["startTime"];
                        c.EndTime = (DateTime)reader["endTime"];
                        c.Callsign = (string)reader["callsign"];
                        c.Station = reader["station"] as string;
                        c.Operator = reader["operator"] as string;
                        c.Band = BandHelper.Parse(reader["band"] as string);
                        c.Mode = ModeHelper.Parse(reader["mode"] as string);
                        c.Frequency = (long)reader["frequency"];
                        c.ReportReceived = (reader["reportRx"] as string) ?? string.Empty;
                        c.ReportSent = (reader["reportTx"] as string) ?? string.Empty;
                        c.Notes = (reader["notes"] as string) ?? string.Empty;
                        int serialReceived;
                        int serialSent;
                        int.TryParse(reader["serialReceived"] as string, out serialReceived);
                        int.TryParse(reader["serialSent"] as string, out serialSent);
                        c.SerialReceived = serialReceived;
                        c.SerialSent = serialSent;
                        c.QslRxDate = reader.GetDateTimeNullable("qslRxDate");
                        c.QslTxDate = reader.GetDateTimeNullable("qslTxDate");
                        c.QslMethod = reader["qslMethod"] as string;

                        // Optional stuff below here...
                        string locatorString = reader["locator"] as string;
                        if (locatorString != null)
                            c.LocatorReceived = new Locator(locatorString);
                        
#warning reintroduce serials and points
                        //c.Points = (int)reader["points"];
                        //c.SerialReceived = (int)reader["serialReceived"];
                        //c.SerialSent = (int)reader["serialSent"];
                        //c.IotaRef = reader["iotaRef"] as string;

                        return c;
                    }
                }
            }
        }

        public void SaveContact(Contact c)
        {
            lock (m_Connection)
            {
                using (MySqlCommand cmd = m_Connection.CreateCommand())
                {
                    if (c.Id > 0)
                        cmd.CommandText = @"UPDATE log SET lastModified=?lastModified, startTime=?startTime, endTime=?endTime,
                            callsign=?callsign, station=?station, operator=?operator, band=?band, mode=?mode, frequency=?frequency,
                            reportTx=?reportTx, reportRx=?reportRx, locator=?locatorReceived, notes=?notes, serialSent=?serialSent, serialReceived=?serialReceived, qslRxDate=?qslRxDate, 
                            qslTxDate=?qslTxDate, qslMethod=?qslMethod WHERE id=?id AND sourceId=?sourceId;";
                    else
                        cmd.CommandText = @"INSERT INTO log (sourceId, lastModified, startTime, endTime, callsign, station, 
                            operator, band, mode, frequency, reportTx, reportRx, locator, notes, serialSent, serialReceived, qslRxDate, qslTxDate, qslMethod) VALUES 
                            (?sourceId, ?lastModified, ?startTime, ?endTime, ?callsign, ?station, ?operator, ?band, ?mode, ?frequency,
                            ?reportTx, ?reportRx, ?locatorReceived, ?notes, ?serialSent, ?serialReceived, ?qslRxDate, ?qslTxDate, ?qslMethod);";

                    cmd.Parameters.AddWithValue("?sourceId", c.SourceId);
                    cmd.Parameters.AddWithValue("?lastModified", DateTime.Now); c.LastModified = DateTime.Now;
                    cmd.Parameters.AddWithValue("?startTime", c.StartTime);
                    cmd.Parameters.AddWithValue("?endTime", c.EndTime);
                    cmd.Parameters.AddWithValue("?callsign", c.Callsign);
                    cmd.Parameters.AddWithValue("?station", c.Station);
                    cmd.Parameters.AddWithValue("?operator", c.Operator);
                    cmd.Parameters.AddWithValue("?band", BandHelper.ToString(c.Band));
                    cmd.Parameters.AddWithValue("?mode", ModeHelper.ToString(c.Mode));
                    cmd.Parameters.AddWithValue("?frequency", c.Frequency);
                    cmd.Parameters.AddWithValue("?reportRx", c.ReportReceived);
                    cmd.Parameters.AddWithValue("?reportTx", c.ReportSent);
                    cmd.Parameters.AddWithValue("?locatorReceived", c.LocatorReceived.ToString());
                    cmd.Parameters.AddWithValue("?notes", c.Notes);
                    cmd.Parameters.AddWithValue("?serialSent", c.SerialSent.ToString());
                    cmd.Parameters.AddWithValue("?serialReceived", c.SerialReceived.ToString());
                    cmd.Parameters.AddWithValue("?qslRxDate", c.QslRxDate);
                    cmd.Parameters.AddWithValue("?qslTxDate", c.QslTxDate);
                    cmd.Parameters.AddWithValue("?qslMethod", c.QslMethod);
                    //cmd.Parameters.AddWithValue("?points", c.Points);
                    //cmd.Parameters.AddWithValue("?serialReceived", c.SerialReceived);
                    //cmd.Parameters.AddWithValue("?serialSent", c.SerialSent);
                    //cmd.Parameters.AddWithValue("?iotaRef", c.IotaRef);
                    
                    cmd.Parameters.AddWithValue("?id", c.Id);
                    cmd.ExecuteNonQuery();
                    c.Id = (int)cmd.LastInsertedId;
                }
            }
        }

        public List<Contact> GetLatestContacts(int maxToFetch)
        {
            lock (m_Connection)
            {
                List<Contact> contacts = new List<Contact>();
                List<KeyValuePair<int, int>> contactIds = new List<KeyValuePair<int, int>>();
                using (MySqlCommand cmd = m_Connection.CreateCommand())
                {
                    cmd.CommandText = "SELECT sourceId, id FROM log ORDER BY endTime DESC LIMIT " + maxToFetch;
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            contactIds.Add(new KeyValuePair<int, int>(reader.GetInt32(0), reader.GetInt32(1)));
                        }
                    }
                }
                foreach (KeyValuePair<int, int> contactDetails in contactIds)
                    contacts.Add(LoadContact(contactDetails.Key, contactDetails.Value));
                return contacts;
            }
        }

        public int GetSerial(Band band)
        {
            lock (m_Connection)
            {
                int nextSerial;
                bool needToCreate;
                using (MySqlCommand cmd = m_Connection.CreateCommand())
                {
                    cmd.CommandText = "SELECT nextSerial FROM serials WHERE band=?band;";
                    cmd.Parameters.AddWithValue("?band", BandHelper.ToString(band));
                    object nextSerialObj = cmd.ExecuteScalar();
                    if (nextSerialObj is int)
                    {
                        nextSerial = (int)nextSerialObj;
                        needToCreate = false;
                    }
                    else
                    {
                        nextSerial = 1;
                        needToCreate = true;
                    }
                }

                using (MySqlCommand cmd = m_Connection.CreateCommand())
                {
                    if (needToCreate)
                        cmd.CommandText = "INSERT INTO serials (band, nextSerial) VALUES (?band, ?nextSerial);";
                    else
                        cmd.CommandText = "UPDATE serials SET nextSerial=?nextSerial WHERE band=?band;";

                    cmd.Parameters.AddWithValue("?band", BandHelper.ToString(band));
                    cmd.Parameters.AddWithValue("?nextSerial", nextSerial + 1);
                    cmd.ExecuteNonQuery();
                }

                return nextSerial;
            }
        }

        public List<Band> GetPreviousBands(string callsign, out Locator locator)
        {
            List<Contact> previousContacts = GetPreviousContacts(callsign);
            List<Band> previousBands = new List<Band>();
            locator = null;
            foreach (Contact c in previousContacts)
            {
                if (c.Band != Band.Unknown && !previousBands.Contains(c.Band))
                    previousBands.Add(c.Band);
                locator = c.LocatorReceived;
            }
            return previousBands;
        }

        public string GetPreviousIota (string callsign)
        {
            foreach (Contact c in GetPreviousContacts(callsign))
            {
                if (!string.IsNullOrEmpty(c.IotaRef))
                    return c.IotaRef;
            }
            return null;
        }

        public Locator GetPreviousLocator(string callsign)
        {
            foreach (Contact c in GetPreviousContacts(callsign))
            {
                if (!string.IsNullOrEmpty(c.IotaRef))
                    return c.LocatorReceived;
            }
            return null;
        }

        public List<Contact> GetPreviousContacts(string callsign)
        {
            lock (m_Connection)
            {
                List<KeyValuePair<int, int>> contactIds = new List<KeyValuePair<int, int>>();
                using (MySqlCommand cmd = m_Connection.CreateCommand())
                {
                    cmd.CommandText = "SELECT callsign, sourceId, id FROM log WHERE callsign=?callsign OR substr(callsign, 1, length(callsign) - 2)=?callsign;";
                    cmd.Parameters.AddWithValue("?callsign", callsign);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string callDB = reader.GetString(0);
                            if (!callDB.Equals(callsign, StringComparison.InvariantCultureIgnoreCase))
                            {
                                if (!(callDB.EndsWith("/p", StringComparison.InvariantCultureIgnoreCase) || callDB.EndsWith("/m", StringComparison.InvariantCultureIgnoreCase)))
                                    continue;
                            }

                            contactIds.Add(new KeyValuePair<int, int>(reader.GetInt32(1), reader.GetInt32(2)));
                        }
                    }
                }

                List<Contact> previousContacts = new List<Contact>(contactIds.Count);
                foreach (KeyValuePair<int, int> id in contactIds)
                    previousContacts.Add(LoadContact(id.Key, id.Value));
                return previousContacts;
            }
        }

        public List<string> GetPartialMatchesThisContest(string callsign)
        {
            return GetPartialMatches(callsign, "log");
        }

        public List<string> GetLocatorMatchesThisContest(string locator)
        {
            return GetLocatorMatches(locator, "log");
        }

        public List<string> GetPartialMatchesKnownCalls(string callsign)
        {
            return GetPartialMatches(callsign, "knowncalls");
        }

        private List<string> GetPartialMatches(string callsign, string table)
        {
            List<string> matches = new List<string>();
            using (MySqlCommand cmd = m_Connection.CreateCommand())
            {
                cmd.CommandText = "SELECT callsign, MAX(locator) AS loc FROM " + table + " WHERE callsign LIKE ?callsign GROUP BY callsign ORDER BY callsign";
                cmd.Parameters.AddWithValue("?callsign", string.Format("%{0}%", callsign.ToUpperInvariant()));
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string call = reader.GetString(0);
                        string loc = reader.GetString(1);
                        matches.Add(call + " - " + loc);
                    }
                }
            }
            return matches;
        }

        private List<string> GetLocatorMatches(string locator, string table)
        {
            List<string> matches = new List<string>();
            using (MySqlCommand cmd = m_Connection.CreateCommand())
            {
                cmd.CommandText = "SELECT callsign, MAX(locator) AS loc FROM " + table + " WHERE locator LIKE ?locator GROUP BY callsign ORDER BY callsign";
                cmd.Parameters.AddWithValue("?locator", string.Format("%{0}%", locator.ToUpperInvariant()));
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string call = reader.GetString(0);
                        string loc = reader.GetString(1);
                        matches.Add(call + " - " + loc);
                    }
                }
            }
            return matches;
        }

        public bool IsNewSquare(string locator, Band band)
        {
            if (locator.Length > 4)
                locator = locator.Substring(0, 4);

            using (MySqlCommand cmd = m_Connection.CreateCommand())
            {
                cmd.CommandText = "SELECT COUNT(*) FROM log WHERE band LIKE ?band AND locator LIKE ?locator;";
                cmd.Parameters.AddWithValue("?band", BandHelper.ToString(band));
                cmd.Parameters.AddWithValue("?locator", locator.ToUpperInvariant() + "%");
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (!reader.Read())
                        return false;
                    return reader.GetInt32(0) == 0;
                }
            }
        }

        public void ImportKnownCalls(string filename)
        {
            string[] lines = File.ReadAllLines(filename);
            using (MySqlCommand cmd = m_Connection.CreateCommand())
            {
                cmd.CommandText = "INSERT IGNORE INTO knowncalls (callsign) VALUES (?callsign)";
                cmd.Parameters.Add("?callsign", MySqlDbType.VarChar);
                foreach (string callsign in lines)
                {
                    cmd.Parameters["?callsign"].Value = callsign.Trim().ToUpperInvariant();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<Contact> GetAllContacts(string station)
        {
            lock (m_Connection)
            {
                List<KeyValuePair<int, int>> contactIDs = new List<KeyValuePair<int, int>>();
                using (MySqlCommand cmd = m_Connection.CreateCommand())
                {
                    cmd.CommandText = "SELECT sourceId, id FROM log WHERE station LIKE ?station ORDER BY endTime";
                    cmd.Parameters.AddWithValue("station", station);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                            contactIDs.Add(new KeyValuePair<int, int>(reader.GetInt32(0), reader.GetInt32(1)));
                    }
                }

                List<Contact> contacts = new List<Contact>(contactIDs.Count);
                foreach (KeyValuePair<int, int> id in contactIDs)
                    contacts.Add(LoadContact(id.Key, id.Value));
                return contacts;
            }
        }

        public Dictionary<string, int> GetFrequencies()
        {
            lock (m_Connection)
            {
                using (MySqlCommand cmd = m_Connection.CreateCommand())
                {
                    cmd.CommandText = "SELECT station, frequency FROM frequencies ORDER BY frequency ASC";
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        Dictionary<string, int> frequencies = new Dictionary<string,int> ();
                        while (reader.Read())
                            frequencies[reader["station"] as string] = (int)reader["frequency"];
                        return frequencies;
                    }
                }
            }
        }

        public void SetFrequency(string station, int frequency)
        {
            lock (m_Connection)
            {
                using (MySqlCommand cmd = m_Connection.CreateCommand())
                {
                    cmd.CommandText = "UPDATE frequencies SET frequency=?frequency WHERE station=?station";
                    cmd.Parameters.AddWithValue("?station", station);
                    cmd.Parameters.AddWithValue("?frequency", frequency);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<Band> GetAllBands()
        {
            lock (m_Connection)
            {
                using (MySqlCommand cmd = m_Connection.CreateCommand())
                {
                    cmd.CommandText = "SELECT DISTINCT band FROM log;";
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        List<Band> bands = new List<Band>();
                        while (reader.Read())
                            bands.Add(BandHelper.Parse(reader.GetString(0)));
                        return bands;
                    }
                }
            }
        }

        public string ExportLog(Locator sourceLocator, Band band)
        {
            lock (m_Connection)
            {
                List<KeyValuePair<int, int>> contactIDs = new List<KeyValuePair<int, int>>();
                using (MySqlCommand cmd = m_Connection.CreateCommand())
                {
                    cmd.CommandText = "SELECT sourceId, id FROM log WHERE band=?band ORDER BY startTime";
                    cmd.Parameters.AddWithValue("?band", BandHelper.ToString(band));
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                            contactIDs.Add(new KeyValuePair<int, int>(reader.GetInt32(0), reader.GetInt32(1)));
                    }
                }

                StringBuilder sb = new StringBuilder();
                List<string> locator4SquaresSeen = new List<string> ();
                int totalPoints = 0;
                int oDxPoints = 0;
                Contact oDxContact = null;
                foreach (KeyValuePair<int, int> id in contactIDs)
                {
                    int pts;
                    Contact c = LoadContact(id.Key, id.Value);
                    sb.AppendLine(GetContactLog(c, sourceLocator, locator4SquaresSeen, out pts));
                    totalPoints += pts;
                    if (pts > oDxPoints)
                    {
                        oDxContact = c;
                        oDxPoints = pts;
                    }
                }

                // Write the header, now we have the various info
                Reg1TestHeader header = new Reg1TestHeader
                {
                    Antenna = "ANTENNA",
                    Band = band,
                    Callsign = "G3PYE/P",
                    Club = "Camb-Hams",
                    ContactAddress1 = "13 Harlestones Road",
                    ContactAddress2 = "Cottenham",
                    ContactCall = "M0VFC",
                    ContactCity = "Cambridge",
                    ContactCounty = string.Empty,
                    ContactEmail = "robert@m0vfc.co.uk",
                    ContactName = "Robert Chipperfield",
                    ContactPhone = "07990 646923",
                    ContactPostCode = "CB24 8TR",
                    ContestName = "VHF NFD BAND",
                    EndDate = new DateTime(2010, 07, 04),
                    HeightAboveGround = 20,
                    HeightAboveSea = 115,
                    Locator = new Locator("JO02ED"),
                    Multipliers = locator4SquaresSeen.Count,
                    OdxCall = oDxContact.Callsign,
                    OdxLocator = oDxContact.LocatorReceived,
                    OdxDistance = oDxPoints,
                    Operators = "G1SAA,G3ZAY,G4ERO,G6KWA,G7VJR,G8IDL,G8TMV,M/DD2YCS,M0MVB,M0NKM,M0VFC,M1BXF",
                    Points = totalPoints,
                    Power = 400,
                    Qsos = contactIDs.Count,
                    Receiver = "RECEIVER",
                    Section = "Open",
                    StartDate = new DateTime(2010, 07, 03),
                    TotalScore = totalPoints * locator4SquaresSeen.Count,
                    Transmitter = "TRANSMITTER"
                };

                sb.Insert(0, header.HeaderText);
                return sb.ToString();
            }
        }

        //private string GetContactLog(Contact c)
        //{
        //    string thisEntry = string.Format("{0} {1} {2} {3} {4} {5} {6} {7} {8} {9} {10} {11} {12} {13} {14} {15} {16} {17} {18}",
        //        c.Time.ToString("yyMMdd"),
        //        c.Time.ToString ("HHmm"),
        //        BandHelper.ToMHzString (c.Band).PadRight(4),
        //        ModeHelper.ToOfficialString(c.Mode).PadRight(3),
        //        c.Callsign.PadRight (15),
        //        c.ReportSent.PadRight (3),
        //        c.SerialSent.ToString().PadRight (4),
        //        c.ReportReceived.PadRight (3),
        //        c.SerialReceived.ToString().PadRight (4),
        //        string.Empty.PadRight (4) /* bonus multiplier etc */,
        //        c.Points.ToString().PadRight (4),
        //        c.Operator.PadRight(6),
        //        c.LocatorReceived.ToString(),
        //        string.Empty.PadRight (1) /* locator multiplier */,
        //        string.Empty.PadRight (3) /* postcode */,
        //        string.Empty.PadRight(1) /* postcode mult */,
        //        string.Empty.PadRight (3) /* country code */,
        //        string.Empty.PadRight (1) /* country code mult */,
        //        c.Notes + "<CE>"
        //        );
        //    return thisEntry;
        //}



        private string GetContactLog(Contact c, Locator sourceLocator, List<string> locator4SquaresSeen, out int points)
        {
            // Figure out if this QSO is valid as a mult
            bool qualifiesForMult;
            PrefixRecord prefix = m_CallsignLookup.LookupPrefix(c.Callsign.Trim());
            if (prefix == null)
            {
                qualifiesForMult = false;
            }
            else
            {
                switch (prefix.Entity)
                {
                    case "ENGLAND":
                    case "NORTHERN IRELAND":
                    case "WALES":
                    case "SCOTLAND":
                    case "ISLE OF MAN":
                    case "JERSEY":
                    case "GUERNSEY":
                        qualifiesForMult = true;
                        break;
                    default:
                        qualifiesForMult = false;
                        break;
                }
            }

            string square4 = c.LocatorReceived.ToString().Substring(0, 4);
            bool newSquare = qualifiesForMult && !locator4SquaresSeen.Contains(square4.ToLowerInvariant());
            if (newSquare)
                locator4SquaresSeen.Add(square4.ToLowerInvariant());

            string thisEntry = string.Format("{0};{1};{2};{3};{4};{5};{6};{7};{8};{9};{10};{11};{12};{13};{14}",
                c.EndTime.ToString("yyMMdd"),
                c.EndTime.ToString("HHmm"),
                c.Callsign,
                string.Empty /* mode code */,
                c.ReportSent,
                c.SerialSent,
                c.ReportReceived,
                c.SerialReceived,
                string.Empty /* received exchange */,
                c.LocatorReceived.ToString().ToUpperInvariant(),
                points = (int)Math.Ceiling(Geographics.GeodesicDistance(sourceLocator, c.LocatorReceived) / 1000),
                string.Empty /* new exchange */,
                newSquare ? "N" : string.Empty /* new locator square */,
                string.Empty /* new DXCC */,
                string.Empty /* dupe */);
            return thisEntry;
        }

        public void Dispose()
        {
            if (m_Connection != null)
            {
                m_Connection.Dispose();
                m_Connection = null;
            }
        }
    }
}
