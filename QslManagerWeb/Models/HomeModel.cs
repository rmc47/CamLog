using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QslManagerWeb.Models
{
    public class HomeModel
    {
        public List<KeyValuePair<DateTime, int>> DatesWithMissingLocations { get; set; }
    }
}