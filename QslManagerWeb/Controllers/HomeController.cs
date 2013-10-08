using Engine;
using QslManagerWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QslManagerWeb.Controllers
{
    public class HomeController : Controller
    {
        private ContactStore m_ContactStore;

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            m_ContactStore = Session["ContactStore"] as ContactStore;
            if (m_ContactStore == null)
            {
                filterContext.Result = Redirect("~/Login");
                return;
            }
            base.OnActionExecuting(filterContext);
        }

        public ActionResult Index()
        {
            HomeModel model = new HomeModel();
            List<Contact> contactsWithoutLocation = m_ContactStore.GetContactsByLocation(null);
            var dates = contactsWithoutLocation.GroupBy(c => c.StartTime.Date).Select(grouping => new { Date = grouping.Key, QsoCount = grouping.Count() }).OrderBy(grouping => grouping.Date).Select(grouping => new KeyValuePair<DateTime, int>(grouping.Date, grouping.QsoCount));
            model.DatesWithMissingLocations = dates.ToList();
            return View(model);
        }
    }
}
