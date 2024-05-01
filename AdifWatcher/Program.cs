using Engine;

List<string> paths = new List<string>();
string? host = null, user = null, pass = null, db = null, station = null, opcall = null, satname = null, satmode = null, transverteroffset = null;
foreach (string arg in args)
{
    if (arg.StartsWith("--"))
    {
        if (arg.Contains('='))
        {
            int sepPos = arg.IndexOf('=');
            string argval = arg.Substring(sepPos + 1);
            switch (arg.Substring(0, sepPos).ToLowerInvariant())
            {
                case "--host": host = argval; break;
                case "--user": user = argval; break;
                case "--pass": pass = argval; break;
                case "--db": db = argval; break;
                case "--station": station = argval; break;
                // Overrides...
                case "--operator": opcall = argval; break;
                case "--satname": satname = argval; break;
                case "--satmode": satmode = argval; break;
                case "--transverteroffset": transverteroffset = argval; break;
                default: throw new ArgumentException("Unknown argument: " + arg);
            }
        }
    }
    else
    {
        paths.Add(arg);
    }
}

if (host == null || user == null || db == null || station == null)
{
    Console.WriteLine("Usage: AdifWatcher.exe --host=<MariaDB hostname> --user=<DB user> --pass=<DB password> --db=<DB name> --station=HF1 [--operator=M0VFC] [--satname=QO-100] [--satmode=SX] [--transverteroffset=1967500000] <path to ADIF>[,<path to ADIF>...]");
    return;
}

ContactStore cs = new ContactStore(host, db, user, pass);

Dictionary<AdifFileReader.Record, int> importedContacts = new Dictionary<AdifFileReader.Record, int>();
while(true)
{
    foreach(string path in paths)
    {
        Console.WriteLine("Checking {0}", path);
        var adif = AdifFileReader.LoadFromContent(File.ReadAllText(path));
        adif.ReadHeader();
        AdifFileReader.Record r;
        while ((r = adif.ReadRecord()) != null)
        {
            if (!importedContacts.ContainsKey(r))
            {
                var contact = AdifHandler.GetContact(r);
                contact.SourceId = cs.SourceId;
                if (station != null)
                    contact.Station = station;
                if (satname != null)
                    contact.SatelliteName = satname;
                if (satmode != null)
                    contact.SatelliteMode = satmode;
                if (opcall != null)
                    contact.Operator = opcall.ToUpperInvariant();
                if (transverteroffset != null)
                {
                    contact.Frequency += long.Parse(transverteroffset);
                    contact.Band = BandHelper.FromFrequency(contact.Frequency);
                }

                var previousContacts = cs.GetPreviousContacts(contact.Callsign);
                if (!previousContacts.Any(c => Contact.QsoMatchEquals(c, contact)))
                {
                    // Not yet in the log
                    cs.SaveContact(contact);
                    Console.WriteLine("Imported new contact: {0} by {1}", contact.Callsign, contact.Operator);
                }
                importedContacts[r] = 0;
            }
        }
    }
    Console.WriteLine("Completed");
    Thread.Sleep(10000);
}