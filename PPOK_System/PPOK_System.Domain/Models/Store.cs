using System.Collections.Generic;

// Represents a Pharmacy in the database, that has a list of pharmacists attached to it
namespace PPOK_System.Domain.Models {
	public class Store {
		public int? store_id { get; set; }
		public string name { get; set; }
		public string address { get; set; }
		public string city { get; set; }
		public string state { get; set; }
		public string zip { get; set; }

		public List<Person> pharmacists;
	}
}