using Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QslManagerWeb.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /Login/

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string server, string database, string username, string password)
        {
            ContactStore cs = new ContactStore(server, database, username, password);
            Session["ContactStore"] = cs;
            return Redirect("~/");
        }

    }
}
