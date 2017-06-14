using PPOK_System.Models;
using PPOK_System.Domain.Models;
using PPOK_System.Domain.Service;
using PPOK_System.Service.Authentication;
using PPOK_System.TwilioManager;
using System.Web.Mvc;
using System.Web.Security;
using PPOK_System.Service.Models;

namespace PPOK_System.Controllers {
    public class HomeController : BaseController {
		Database db = new Database(SystemContext.DefaultConnectionString);

        // GET: Home
        public ActionResult Index() {
            //FormsAuthentication.SignOut();
            TwManager tw = new TwManager();

            tw.StartHangfire();
            return RedirectToAction("Login");
        }


		// GET: Home/Login
		[HttpGet]
		public ActionResult Login() {
			if (User != null) {
				if (User.IsInRole("Admin")) {
					return RedirectToAction("Index", "Admin");
				} else if (User.IsInRole("Pharmacist")) {
					return RedirectToAction("Index", "Pharmacy");
				} else {
					return RedirectToAction("Index", "User");
				}
			}

			return View();
		}


		// POST: Home/Login/{person}
		[HttpPost]
		public ActionResult Login(Person loginAttempt) {
			var person = db.ReadSinglePerson(loginAttempt.email);

			if (person != null && Password.Authenticate(loginAttempt.password, person.password)) {
				UserPrincipalSerialize user = new UserPrincipalSerialize(person);
				Response.Cookies.Add(AuthTicket.Make(user));

				if (user.IsInRole("Admin")) {
					return RedirectToAction("Index", "Admin");
				} else if (user.IsInRole("Pharmacist")) {
					return RedirectToAction("Index", "Pharmacy");
				} else {
					return RedirectToAction("Index", "User");
				}
			} else {
				ModelState.AddModelError("", "Login data is incorrect!");
			}

			return View(loginAttempt);
		}


		// GET: Home/Logout
		public ActionResult Logout() {
			FormsAuthentication.SignOut();
			return RedirectToAction("Login", "Home");
		}
	}
}