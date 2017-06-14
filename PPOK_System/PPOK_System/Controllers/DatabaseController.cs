using PPOK_System.Domain.Service;
using System.Web.Mvc;
using PPOK_System.TwilioManager;
using PPOK_System.Models;

namespace PPOK_System.Controllers {
	public class DatabaseController : BaseController {
		Database db = new Database(SystemContext.DefaultConnectionString, SystemContext.MasterConnectionString);
		

		// GET: Database
		public ActionResult Index() {
            db.initDatabase();
            return RedirectToAction("Index", "Home");
		}
	}
}