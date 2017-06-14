using System;
using System.Collections.Generic;
using System.IO;
using PPOK_System.Domain.Models;
using PPOK_System.Domain.Service;
using PPOK_System.Models;

namespace PPOK_System.Service.Import {
	public class ImportRecall {
		private static Database db = new Database(SystemContext.DefaultConnectionString);

		public static List<Prescription> Read(StreamReader reader) {
			return DetermineContent(Csv(reader));
		}


		private static List<LineContent> Csv(StreamReader reader) {
			List<LineContent> fileContents = new List<LineContent>();
			reader.ReadLine();

			// read until file is ended
			while (!reader.EndOfStream) {
				var lineContent = new LineContent();
				string pattern = "yyyyMMdd";

				// read in CSV file and create objects
				var line = reader.ReadLine();
				var values = line.Split(',');

				lineContent.DrugID = values[0];
				DateTime dt = DateTime.Now;
				if (DateTime.TryParseExact(values[1], pattern, null, System.Globalization.DateTimeStyles.None, out dt))
					lineContent.BeginDate = dt;
				else
					lineContent.BeginDate = DateTime.Now;

				if (DateTime.TryParseExact(values[2], pattern, null, System.Globalization.DateTimeStyles.None, out dt))
					lineContent.EndDate = dt;
				else
					lineContent.EndDate = DateTime.Now;

				fileContents.Add(lineContent);
			}

			return fileContents;
		}


		private static List<Prescription> DetermineContent(List<LineContent> contents) {
			List<Prescription> updateList = new List<Prescription>();
			foreach (var c in contents)
				updateList.AddRange(db.ReadAllPrescriptionsForDates(c.DrugID, c.BeginDate, c.EndDate));
			return updateList;
		}


		public static void SendMessages(List<Prescription> contents) {
			// TODO send messages
		}


		private class LineContent {
			public string DrugID { get; set; }
			public DateTime BeginDate { get; set; }
			public DateTime EndDate { get; set; }
		}
	}
}