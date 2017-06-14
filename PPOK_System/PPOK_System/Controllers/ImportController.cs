using PPOK_System.import;
using PPOK_System.Domain.Models;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;

namespace PPOK_System.Controllers {
    public class ImportController : BaseController {

		// POST: Import
		[HttpPost]
		public ActionResult Index(HttpPostedFileBase file) {
			var updateList = new List<Prescription>();
			if (file.ContentLength > 0) {
				var isRecall = ImportHandler.IsRecallFile(file);
				updateList = ImportHandler.Handle(file, User.Store.store_id, isRecall);
				ViewBag.IsRecall = isRecall;
			} else {
				ViewBag.IsRecall = null;
			}

			return PartialView(updateList);
		}


		// POST: Import/Upload
		[HttpPost]
		public ActionResult Upload(List<Prescription> updateList, bool isRecall=false) {
			if (updateList != null)
				ImportHandler.Update(updateList, isRecall);
			return Redirect("/Pharmacy");
		}
	}
}