using PPOK_System.Domain.Models;
using PPOK_System.Domain.Service;
using PPOK_System.Models;
using System.Collections.Generic;
using System.IO;
using System.Web;
using PPOK_System.Service.Import;

namespace PPOK_System.import {
	public class ImportHandler {
		private static Database db = new Database(SystemContext.DefaultConnectionString);

		public static List<Prescription> Handle(HttpPostedFileBase file, int? store, bool isRecall=false) {
			StreamReader reader = new StreamReader(file.InputStream);

			if (!isRecall) {
				var results = ImportCustomer.Csv(reader);
				return ImportCustomer.DetermineContent(results, store);
			} else
				return ImportRecall.Read(reader);
		}


		public static bool IsRecallFile(HttpPostedFileBase file) {
			StreamReader reader = new StreamReader(file.InputStream);
			var line = reader.ReadLine();       // initialize first row
			var values = line.Split(',');
			reader.BaseStream.Position = 0;
			return values[0] == "NDCUPCHRI";
		}


		public static void Update(List<Prescription> contents, bool isRecall) {
			if (isRecall)
				ImportRecall.SendMessages(contents);
			else
				ImportCustomer.UpdateContent(contents);
		}
	}
}