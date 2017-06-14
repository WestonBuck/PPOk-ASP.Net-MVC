using PPOK_System.Models;
using PPOK_System.Domain.Models;
using PPOK_System.Domain.Service;
using System.Web.Mvc;
using System.Collections.Generic;

namespace PPOK_System.Controllers
{
    public class AdminController : BaseController
    {
       // int? tempStoreID;

        Database db = new Database(SystemContext.DefaultConnectionString);
        // GET: Admin
        public ActionResult Index()
        {
            var s = db.ReadAllStores();
            return View(s);
        }

        public ActionResult EditPharmacy(int id)
        {
            var s = db.ReadSingleStore(id);
            return PartialView(s);
        }


        //See why this isn't updating anymore
        [HttpPost]
        public ActionResult EditPharmacy(Store s)
        {
            db.Update(s);
            return RedirectToAction("Index", "Admin");
        }

        public ActionResult AddPharmacy()
        {
            return PartialView();
        }

        //get count of all store, add 1 to get new store id, make a dummy person for the new store.
        [HttpPost]
        public ActionResult AddPharmacy2(Store s)
        {
            List<PPOK_System.Domain.Models.Store> temp = new List<PPOK_System.Domain.Models.Store>();
            PPOK_System.Domain.Models.Person p = new PPOK_System.Domain.Models.Person();
            int? storeID;

            temp = db.ReadAllStores();
            storeID = temp.Count + 1;
            
            p.store_id = storeID;
            p.phone = "2222222222";
            p.person_id = null;
            p.last_name = " ";
            p.password = " ";
            p.zip = " ";
            p.email = " ";
            p.person_type = "dummy";
            p.date_of_birth = System.DateTime.Now;

            db.Create(s);
            db.Create(p);
            return RedirectToAction("Index", "Admin");
        }

        public ActionResult AddPharmacist(int id)
        {
            //tempStoreID = id;
            var p = new Person();
            p.store_id = id;
            p.person_type = "Pharmacist";
            return PartialView(p);
        }

        [HttpPost]
        public ActionResult AddPharmacist2(Person p)
        {
           // p.store_id = tempStoreID;
            db.Create(p);
            return RedirectToAction("Index", "Admin");
        }
    }
}