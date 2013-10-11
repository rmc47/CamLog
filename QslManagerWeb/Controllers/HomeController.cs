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

        public ActionResult AllLocations()
        {
            AllLocationsModel model = new AllLocationsModel();
            model.Locations = m_ContactStore.GetLocations();
            return View(model);
        }

        public ActionResult QsosWithoutLocation(DateTime date)
        {
            QsosModel model = new QsosModel();
            model.Title = date.ToString();
            model.Contacts = m_ContactStore.GetAllContacts(null).Where(c => c.StartTime.Date == date.Date && c.LocationID == 0).ToList();
            model.Locations = m_ContactStore.GetLocations();
            return View("Qsos", model);
        }

        [HttpPost]
        public ActionResult AssignQsos(AssignQsosModel model)
        {
            Location location;
            switch (model.LocationType.ToLowerInvariant())
            {
                case "new":
                    location = m_ContactStore.CreateLocation(model.Club, model.Locator, model.Wab, "EU-005", "Great Britain");
                    break;
                case "existing":
                    location = m_ContactStore.LoadLocation(model.ExistingLocationId);
                    break;
                default:
                    throw new ArgumentException("Unknown location type");
            }

            var qsoIDs = model.QsoIDs.Split(',').SelectMany(id => {
                if (string.IsNullOrWhiteSpace(id))
                    return new int[0];
                else
                    return new[] { int.Parse(id) };
            });

            // TODO: QSO IDs should be passed in with source IDs somehow
            var contacts = qsoIDs.Select(id => m_ContactStore.LoadContact(m_ContactStore.SourceId, id));
            foreach (var contact in contacts)
            {
                contact.LocationID = location.ID;
                m_ContactStore.SaveContact(contact);
            }
            return RedirectToAction("Index");
        }
    }
}
