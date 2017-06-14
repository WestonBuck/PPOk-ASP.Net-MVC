using PPOK_System.Domain.Models;
using PPOK_System.Domain.Service;
using PPOK_System.Models;
using System.Web.Mvc;

namespace PPOK_System.Controllers {
	public class UserController : BaseController {
		Database db = new Database(SystemContext.DefaultConnectionString);

		// GET: User/
		public ActionResult Index() {
			var p = db.ReadSinglePerson(User.Email);
			return View(p);
		}


		// POST: User/Index/{person}
		[HttpPost]
		public ActionResult Index(Person p) {
			if (p.contact_preference != null)
				db.Update(p.contact_preference);
			return View(p);
		}
    }
}