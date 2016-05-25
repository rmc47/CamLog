using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Reflection;
using System.IO;

namespace Engine.DBMigrations
{
    internal static class DbMigrator
    {
        public static void UpgradeDatabase(DbConnection connection, DatabaseType databaseType)
        {
            // Figure out the current version of the schema
            int currentVersion;
            using (DbCommand cmd = connection.CreateCommand())
            {
                cmd.CommandText = "SELECT val FROM setup WHERE `key`='schemaVersion';";
                using (DbDataReader reader = cmd.ExecuteReader())
                {
                    if (!reader.Read())
                        currentVersion = 1;
                    else
                        currentVersion = int.Parse(reader.GetString(reader.GetOrdinal("val")));
                }
            }

            // Now get the set of migrations we have avaiable
            List<int> migrationVersions = GetMigrationVersions(databaseType);
            foreach (int migration in migrationVersions)
            {
                if (currentVersion < migration)
                {
                    PerformMigration(connection, databaseType, migration);
                }
            }
        }

        public static int LatestSchemaVersion(DatabaseType type)
        {
            List<int> migrationNames = GetMigrationVersions(type);
            return migrationNames[migrationNames.Count - 1];
        }

        private static List<int> GetMigrationVersions(DatabaseType type)
        {
            Assembly ass = Assembly.GetExecutingAssembly();
            string[] resources = ass.GetManifestResourceNames();
            string resourcePrefix = string.Format("Engine.DBMigrations.{0}.", type.ToString());
            List<int> migrationNames = new List<int>();
            foreach (string resourceName in resources)
            {
                if (resourceName.StartsWith(resourcePrefix))
                {
                    string[] resourceNameBits = resourceName.Split('.');
                    migrationNames.Add(int.Parse(resourceNameBits[resourceNameBits.Length - 2])); // Lose the file extension
                }
            }
            migrationNames.Sort();
            return migrationNames;
        }

        private static string GetMigration(DatabaseType type, int version)
        {
            string resourceName = string.Format("Engine.DBMigrations.{0}.{1}.sql", type.ToString(), version);
            Stream resourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName);
            if (resourceStream == null)
                return null;
            using (StreamReader reader = new StreamReader(resourceStream))
            {
                return reader.ReadToEnd();
            }
        }

        private static void PerformMigration(DbConnection connection, DatabaseType databaseType, int migrationVersion)
        {
            using (DbCommand cmd = connection.CreateCommand())
            {
                cmd.CommandText = GetMigration(databaseType, migrationVersion);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
