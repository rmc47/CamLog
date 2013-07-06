using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;
using System.IO;
using System.Threading;

namespace Engine
{
    public class ContactStore : IDisposable
    {
        private string m_ConnectionString;
        private MySqlConnection m_Connection;
        private int m_SourceId;
        private CallsignLookup m_CallsignLookup = new CallsignLookup("cty.xml.gz");

        private MySqlConnection OpenConnection
        {
            get
            {
                MySqlConnection conn = m_Connection;
                // Don't Ping() - it'll interrupt any other threads using this connection and break them!
                if (conn.State == System.Data.ConnectionState.Open/* && conn.Ping()*/)
                {
                    return conn;
                }
                else
                {
                    MySqlConnection newConn = new MySqlConnection(m_ConnectionString);
                    newConn.Open();
                    m_Connection = newConn;
                    return newConn;
                }
            }
        }

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
            m_ConnectionString = csb.ConnectionString;
            m_Connection = new MySqlConnection(csb.ConnectionString);
            m_Connection.Open();

            DBMigrations.DbMigrator.UpgradeDatabase(m_Connection, DatabaseType.MySQL);

            using (MySqlCommand cmd = m_Connection.CreateCommand())
            {
                cmd.CommandText = "SELECT id FROM sources WHERE `default`=1;";
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (!reader.Read() || reader.IsDBNull(0))
                    {
                        m_SourceId = new Random().Next();
                        cmd.CommandText = "INSERT INTO sources (id, callsign, `default`) VALUES (?id, ?callsign, 1);";
                        cmd.Parameters.AddWithValue("?id", m_SourceId);
                        cmd.Parameters.AddWithValue("?callsign", m_SourceId.ToString()); // TODO: should really ask the user for this
                        reader.Close();
                        cmd.ExecuteNonQuery();
                    }
                    else
                    {
                        m_SourceId = reader.GetInt32(0);
                    }
                }
            }
        }

        public int SourceId
        {
            get { return m_SourceId; }
        }

        public Contact LoadContact(int sourceId, int id)
        {
            MySqlConnection conn = OpenConnection;
            lock (conn)
            {
                using (MySqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM log WHERE id=?id AND sourceId=?sourceId;";
                    cmd.Parameters.AddWithValue("?id", id);
                    cmd.Parameters.AddWithValue("?sourceId", sourceId);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (!reader.Read())
                            return null;

                        return LoadContact(reader);
                    }
                }
            }
        }

        private Contact LoadContact(MySqlDataReader reader)
        {
            Contact c = new Contact();
            c.Id = (int)reader["id"];
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
            c.LocationID = (int)reader["location"];

            // Optional stuff below here...
            string locatorString = reader["locator"] as string;
            if (locatorString != null)
                c.LocatorReceived = new Locator(locatorString);

            return c;
        }

        public void SaveContact(Contact c)
        {
            MySqlConnection conn = OpenConnection;
            lock (conn)
            {
                using (MySqlCommand cmd = conn.CreateCommand())
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
                    cmd.Parameters.AddWithValue("?lastModified", DateTime.UtcNow); c.LastModified = DateTime.UtcNow;
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
                    cmd.Parameters.AddWithValue("?locatorReceived", c.LocatorReceivedString ?? (object)DBNull.Value);
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

                    if (c.Id <= 0)
                        c.Id = (int)cmd.LastInsertedId; // This is only valid for an insert, not an update
                }
            }
        }

        public Location LoadLocation(int id)
        {
            MySqlConnection conn = OpenConnection;
            lock (conn)
            {
                using (MySqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM locations WHERE id=?id;";
                    cmd.Parameters.AddWithValue("?id", id);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (!reader.Read())
                            return null;

                        Location l = new Location
                        {
                            ID = id,
                            Club = reader.GetString("club"),
                            IotaName = reader.GetString("iotaname"),
                            IotaRef = reader.GetString("iotaref"),
                            Locator = reader.GetString("locator"),
                            Wab = reader.GetString("wab"),
                        };
                        return l;
                    }
                }
            }
        }

        public List<Contact> GetLatestContacts(int maxToFetch, string station, string band = null, string op = null)
        {
            MySqlConnection conn = OpenConnection;
            lock (conn)
            {
                List<Contact> contacts = new List<Contact>();
                List<KeyValuePair<int, int>> contactIds = new List<KeyValuePair<int, int>>();
                using (MySqlCommand cmd = conn.CreateCommand())
                {
                    string commandText = "SELECT sourceId, id FROM log WHERE TRUE ";
                    if (!string.IsNullOrEmpty(station))
                    {
                        commandText += "AND station=?station OR notes LIKE ?stationNotes ";
                        if (!string.IsNullOrEmpty(band))
                            commandText += "OR notes LIKE ?bandNotes ";
                        if (!string.IsNullOrEmpty(op))
                            commandText += "OR notes LIKE ?opNotes ";
                    }
                    commandText += " ORDER BY endTime DESC LIMIT ?maxToFetch";
                    cmd.CommandText = commandText;

                    cmd.Parameters.AddWithValue("?station", station);
                    cmd.Parameters.AddWithValue("?stationNotes", "%" + station + "%");
                    if (!string.IsNullOrEmpty(band))
                        cmd.Parameters.AddWithValue("?bandNotes", "%" + band + "%");
                    if (!string.IsNullOrEmpty(op))
                        cmd.Parameters.Add("?opNotes", "%" + op + "%");
                    cmd.Parameters.AddWithValue("?maxToFetch", maxToFetch);

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
            MySqlConnection conn = OpenConnection;
            lock (conn)
            {
                int nextSerial;
                bool needToCreate;
                using (MySqlCommand cmd = conn.CreateCommand())
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

                using (MySqlCommand cmd = conn.CreateCommand())
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

        public List<Contact> GetPreviousContacts(string callsign)
        {
            MySqlConnection conn = OpenConnection;
            lock (conn)
            {
                List<KeyValuePair<int, int>> contactIds = new List<KeyValuePair<int, int>>();
                using (MySqlCommand cmd = conn.CreateCommand())
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
                                if (!(callDB.EndsWith("/p", StringComparison.InvariantCultureIgnoreCase) || callDB.EndsWith("/m", StringComparison.InvariantCultureIgnoreCase) || callDB.EndsWith("/a", StringComparison.InvariantCultureIgnoreCase)))
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

        public List<Contact> GetApproximateMatches(string callsign)
        {
            MySqlConnection conn = OpenConnection;
            lock (conn)
            {
                List<KeyValuePair<int, int>> ids = new List<KeyValuePair<int, int>>();
                using (MySqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT sourceId, id FROM log WHERE callsign LIKE ?callsign";
                    MySqlParameter callsignParam = cmd.Parameters.Add("?callsign", MySqlDbType.String);

                    // Firstly, exact match
                    callsignParam.Value = callsign;
                    AddContacts(ids, cmd);

                    // Now for each position, allow wildcard substitution
                    for (int i = 0; i < callsign.Length; i++)
                    {
                        char[] callChars = callsign.ToCharArray();
                        callChars[i] = '%';
                        callsignParam.Value = new string(callChars);
                        AddContacts(ids, cmd);
                    }
                }

                List<Contact> previousContacts = new List<Contact>(ids.Count);
                foreach (KeyValuePair<int, int> id in ids)
                    previousContacts.Add(LoadContact(id.Key, id.Value));
                return previousContacts;
            }
        }

        private static void AddContacts(List<KeyValuePair<int, int>> contactList, MySqlCommand cmd)
        {
            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    contactList.Add(new KeyValuePair<int, int>(reader.GetInt32(0), reader.GetInt32(1)));
                }
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
            MySqlConnection conn = OpenConnection;
            lock (conn)
            {
                List<string> matches = new List<string>();
                using (MySqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT callsign, MAX(locator) AS loc FROM " + table + " WHERE callsign LIKE ?callsign GROUP BY callsign ORDER BY callsign";
                    cmd.Parameters.AddWithValue("?callsign", string.Format("%{0}%", callsign.ToUpperInvariant()));
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string call = reader.GetString(0);
                            string loc;
                            if (reader.IsDBNull(1))
                                loc = string.Empty;
                            else
                                loc = reader.GetString(1);
                            matches.Add(call + " - " + loc);
                        }
                    }
                }
                return matches;
            }
        }

        private List<string> GetLocatorMatches(string locator, string table)
        {
            MySqlConnection conn = OpenConnection;
            lock (conn)
            {
                List<string> matches = new List<string>();
                using (MySqlCommand cmd = conn.CreateCommand())
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
        }

        public bool IsNewSquare(string locator, Band band)
        {
            MySqlConnection conn = OpenConnection;
            lock (conn)
            {
                if (locator.Length > 4)
                    locator = locator.Substring(0, 4);

                using (MySqlCommand cmd = conn.CreateCommand())
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
        }

        public void ImportKnownCalls(string filename)
        {
            MySqlConnection conn = OpenConnection;
            lock (conn)
            {
                string[] lines = File.ReadAllLines(filename);
                using (MySqlCommand cmd = conn.CreateCommand())
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
        }

        public List<Contact> GetAllContacts(string station)
        {
            MySqlConnection conn = OpenConnection;
            lock (conn)
            {
                using (MySqlCommand cmd = conn.CreateCommand())
                {
                    if (station != null)
                    cmd.CommandText = "SELECT * FROM log WHERE station LIKE ?station ORDER BY endTime";
                    else
                        cmd.CommandText = "SELECT * FROM log ORDER BY endTime";
                    cmd.Parameters.AddWithValue("station", station);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        List<Contact> contacts = new List<Contact>();
                        while (reader.Read())
                        {
                            contacts.Add(LoadContact(reader));
                        }
                        return contacts;
                    }
                }
            }
        }

        public Dictionary<string, int> GetFrequencies()
        {
            MySqlConnection conn = OpenConnection;
            lock (conn)
            {
                using (MySqlCommand cmd = conn.CreateCommand())
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
            MySqlConnection conn = OpenConnection;
            lock (conn)
            {
                using (MySqlCommand cmd = conn.CreateCommand())
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
            MySqlConnection conn = OpenConnection;
            lock (conn)
            {
                using (MySqlCommand cmd = conn.CreateCommand())
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
            MySqlConnection conn = OpenConnection;
            lock (conn)
            {
                List<KeyValuePair<int, int>> contactIDs = new List<KeyValuePair<int, int>>();
                using (MySqlCommand cmd = conn.CreateCommand())
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
                    ContestName = "UKAC",
                    EndDate = new DateTime(2010, 07, 04),
                    HeightAboveGround = 20,
                    HeightAboveSea = 68,
                    Locator = new Locator("JO02CE"),
                    Multipliers = locator4SquaresSeen.Count,
                    OdxCall = oDxContact.Callsign,
                    OdxLocator = oDxContact.LocatorReceived,
                    OdxDistance = oDxPoints,
                    Operators = "M0LCM,M0VFC,M0ZRN,M1BXF",
                    Points = totalPoints,
                    Power = 100,
                    Qsos = contactIDs.Count,
                    Receiver = "RECEIVER",
                    Section = "UKAC Restricted",
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
        // string thisEntry = string.Format("{0} {1} {2} {3} {4} {5} {6} {7} {8} {9} {10} {11} {12} {13} {14} {15} {16} {17} {18}",
        // c.Time.ToString("yyMMdd"),
        // c.Time.ToString ("HHmm"),
        // BandHelper.ToMHzString (c.Band).PadRight(4),
        // ModeHelper.ToOfficialString(c.Mode).PadRight(3),
        // c.Callsign.PadRight (15),
        // c.ReportSent.PadRight (3),
        // c.SerialSent.ToString().PadRight (4),
        // c.ReportReceived.PadRight (3),
        // c.SerialReceived.ToString().PadRight (4),
        // string.Empty.PadRight (4) /* bonus multiplier etc */,
        // c.Points.ToString().PadRight (4),
        // c.Operator.PadRight(6),
        // c.LocatorReceived.ToString(),
        // string.Empty.PadRight (1) /* locator multiplier */,
        // string.Empty.PadRight (3) /* postcode */,
        // string.Empty.PadRight(1) /* postcode mult */,
        // string.Empty.PadRight (3) /* country code */,
        // string.Empty.PadRight (1) /* country code mult */,
        // c.Notes + "<CE>"
        // );
        // return thisEntry;
        //}



        private string GetContactLog(Contact c, Locator sourceLocator, List<string> locator4SquaresSeen, out int points)
        {
            // Figure out if this QSO is valid as a mult
            bool qualifiesForMult;
            PrefixRecord prefix = m_CallsignLookup.LookupPrefix(c.Callsign.Trim());
            if (prefix == null || prefix.Entity == null)
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

            string square4;
            if (c.LocatorReceivedString != null && c.LocatorReceivedString.Length >= 4)
                square4 = c.LocatorReceivedString.Substring(0, 4);
            else
                square4 = string.Empty;

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
                c.LocatorReceivedString,
                points = (int)Math.Ceiling(Geographics.GeodesicDistance(sourceLocator, c.LocatorReceived) / 1000),
                string.Empty /* new exchange */,
                newSquare ? "N" : string.Empty /* new locator square */,
                string.Empty /* new DXCC */,
                string.Empty /* dupe */);
            return thisEntry;
        }

        public List<SourceCallsign> GetSources()
        {
            MySqlConnection conn = OpenConnection;
            lock (conn)
            {
                using (MySqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT id, callsign FROM sources;";
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        List<SourceCallsign> sources = new List<SourceCallsign>();
                        while (reader.Read())
                        {
                            int source = reader.GetInt32(reader.GetOrdinal("id"));
                            string callsign = reader.GetString(reader.GetOrdinal("callsign"));
                            sources.Add(new SourceCallsign(source, callsign));
                        }
                        return sources;
                    }
                }
            }
        }

        public List<List<Contact>> GetContactsToQsl(int sourceId)
        {
            List<int> idsToPrint = new List<int>();
            MySqlConnection conn = OpenConnection;
            lock (conn)
            {
                using (MySqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT id FROM log WHERE qslRxDate IS NOT NULL and qslTxDate IS NULL AND sourceId=?sourceId ORDER BY callsign, startTime;";
                    cmd.Parameters.AddWithValue("?sourceId", sourceId);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int contactId = reader.GetInt32(reader.GetOrdinal("id"));
                            idsToPrint.Add(contactId);
                        }
                    }
                }
            }

            List<List<Contact>> contacts = new List<List<Contact>>();
            string currentCallsign = null;
            List<Contact> currentCallsignContacts = null;
            foreach (int id in idsToPrint)
            {
                Contact c = LoadContact(sourceId, id);
                if (!string.Equals(c.Callsign, currentCallsign, StringComparison.InvariantCultureIgnoreCase))
                {
                    if (currentCallsignContacts != null)
                        contacts.Add(currentCallsignContacts);
                    currentCallsign = c.Callsign;
                    currentCallsignContacts = new List<Contact>();
                }
                currentCallsignContacts.Add(c);
            }
            if (currentCallsignContacts != null)
                contacts.Add(currentCallsignContacts);

            return contacts;
        }

        public void MarkQslsSent(List<List<Contact>> contacts)
        {
            List<Contact> combinedContacts = new List<Contact>();
            contacts.ForEach(innerContacts => combinedContacts.AddRange(innerContacts));
            MarkQslsSent(combinedContacts);
        }

        public void MarkQslsSent(List<Contact> contacts)
        {
            MySqlConnection conn = OpenConnection;
            lock (conn)
            {
                using (MySqlTransaction tran = conn.BeginTransaction())
                {
                    using (MySqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.Transaction = tran;
                        cmd.CommandText = "UPDATE log SET qslTxDate=?txDate WHERE id=?id AND sourceId=?sourceId;";
                        MySqlParameter idParam = cmd.Parameters.Add("?id", MySqlDbType.Int32);
                        MySqlParameter sourceIdParam = cmd.Parameters.Add("?sourceId", MySqlDbType.Int32);
                        cmd.Parameters.AddWithValue("?txDate", DateTime.UtcNow);
                        foreach (Contact c in contacts)
                        {
                            idParam.Value = c.Id;
                            sourceIdParam.Value = c.SourceId;
                            cmd.ExecuteNonQuery();
                        }
                    }
                    tran.Commit();
                }
            }
        }

        public int Import(List<Contact> contacts)
        {
            List<Contact> existingContacts = GetAllContacts(null);
            existingContacts.Sort(Contact.QsoMatchCompare);

            // De-dupe QSOs with existing ones in the DB
            int importedCount = 0;
            foreach (Contact c in contacts)
            {
                if (existingContacts.BinarySearch(c, new Contact.QsoMatchComparer()) >= 0)
                    continue;

                SaveContact(c);
                importedCount++;
            }

            return importedCount;
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
