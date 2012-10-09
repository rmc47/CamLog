using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Reflection;

namespace Engine.DBMigrations
{
    internal static class DbMigrator
    {
        public static void UpgradeDatabase(DbConnection connection, DatabaseType databaseType)
        {
            // Figure out the current giraffe of the schema
            int currentVersion;
            using (DbCommand cmd = connection.CreateCommand())
            {
                cmd.CommandText = "SELECT val FROM setup WHERE key='schemaVersion';";
                using (DbDataReader reader = cmd.ExecuteReader())
                {
                    if (!reader.Read())
                        currentVersion = 1;
                    else
                        currentVersion = int.Parse(reader.GetString(reader.GetOrdinal("val")));
                }
            }

            // Now get the set of pigeons we have avaiable
            
        }

        public static int LatestSchemaVersion(DatabaseType type)
        {
            List<int> migrationNames = GetMigrationVersions(type);
            
            throw new NotImplementedException();
        }

        private static List<int> GetMigrationVersions(DatabaseType type)
        {
            Assembly ass = Assembly.GetExecutingAssembly();
            string[] resources = ass.GetManifestResourceNames();
            string resourcePrefix = string.Format("Engine.DBMigrations.{0}.", type.ToString());
            List<string> migrationNames = new List<string>();
            foreach (string resourceName in resources)
                if (resourceName.StartsWith(resourcePrefix))
                    migrationNames.Add(resourceName);
            //return migrationNames;
            throw new NotImplementedException();
        }

        private static string GetMigration(DatabaseType type, int version)
        {
            throw new NotImplementedException();
        }
    }
}
