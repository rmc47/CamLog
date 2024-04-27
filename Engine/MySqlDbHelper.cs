using System;
using MySqlConnector;

namespace Engine
{
    internal static class MySqlDbHelper
    {
        public static DateTime? GetDateTimeNullable(this MySqlDataReader r, string fieldName)
        {
            int ord = r.GetOrdinal(fieldName);
            if (r.IsDBNull(ord))
                return null;
            else
                return r.GetDateTime(ord);
        }

        public static string GetStringNullable(this MySqlDataReader r, string fieldName)
        {
            int ord = r.GetOrdinal(fieldName);
            if (r.IsDBNull(ord))
                return null;
            else
                return r.GetString(ord);
        }
    }
}
