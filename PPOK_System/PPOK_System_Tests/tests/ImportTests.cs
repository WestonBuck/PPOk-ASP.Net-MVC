using Microsoft.VisualStudio.TestTools.UnitTesting;
using PPOK_System.import;
using System.IO;

namespace PPOK_System_Tests.tests {
	[TestClass]
	public class ImportTests {
		[TestMethod]
		public void testCsv() {
			// test file
			string fileName = "../../../../PPOK_System/PPOK_System/data/scrubbed_data.csv";
			StreamReader reader = new StreamReader(fileName);
			var result = ImportHandler.Csv(reader);

			// check size of result
			Assert.AreEqual(result.Count, 979);

			// check zeroth row
			Assert.AreEqual(result[0].customer.first_name, "STEPHNIE");
			Assert.AreEqual(result[0].customer.last_name, "EIDSNESS");
			Assert.AreEqual(result[0].customer.date_of_birth.Year, 2004);
			Assert.AreEqual(result[0].customer.date_of_birth.Month, 10);
			Assert.AreEqual(result[0].customer.date_of_birth.Day, 12);
			Assert.AreEqual(result[0].customer.zip, "98008");

			Assert.AreEqual(result[0].days_supply, 30);
			Assert.AreEqual(result[0].date_filled.Year, 2008);
			Assert.AreEqual(result[0].date_filled.Month, 05);
			Assert.AreEqual(result[0].date_filled.Day, 06);

			Assert.AreEqual(result[0].drug.drug_id, "60505006501");
			Assert.AreEqual(result[0].drug.drug_name, "Omeprazole Cap Delayed Release 20 MG");

			// check fourth row
			Assert.AreEqual(result[4].customer.first_name, "Nina");
			Assert.AreEqual(result[4].customer.last_name, "Paege Dp30");
			Assert.AreEqual(result[4].customer.date_of_birth.Year, 1923);
			Assert.AreEqual(result[4].customer.date_of_birth.Month, 01);
			Assert.AreEqual(result[4].customer.date_of_birth.Day, 26);
			Assert.AreEqual(result[4].customer.zip, "");

			Assert.AreEqual(result[4].days_supply, 25);
			Assert.AreEqual(result[4].date_filled.Year, 2008);
			Assert.AreEqual(result[4].date_filled.Month, 05);
			Assert.AreEqual(result[4].date_filled.Day, 06);

			Assert.AreEqual(result[4].drug.drug_id, "65027225");
			Assert.AreEqual(result[4].drug.drug_name, "Olopatadine HCl Ophth Soln 0.2% (Base Equivalent)");

			reader.Close();
		}
	}
}
