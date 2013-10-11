using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QslManagerWeb.Models
{
    public class AssignQsosModel
    {
        public int ExistingLocationId { get; set; }
        public string LocationType { get; set; }
        public string Club { get; set; }
        public string Locator { get; set; }
        public string Wab { get; set; }
        public string QsoIDs { get; set; }
    }
}