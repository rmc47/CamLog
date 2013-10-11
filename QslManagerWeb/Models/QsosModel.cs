using Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QslManagerWeb.Models
{
    public class QsosModel
    {
        public string Title { get; set; }
        public List<Contact> Contacts { get; set; }
        public List<Location> Locations { get; set; }
    }
}