using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;

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
    }
}
