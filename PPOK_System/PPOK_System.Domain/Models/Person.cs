using System;

// Represents a user in the database (person_type determines admin, pharmacist, customer)
namespace PPOK_System.Domain.Models {
	public class Person {
		public int? person_id { get; set; }
		public int? store_id { get; set; }
		public string first_name { get; set; }
		public string last_name { get; set; }
		public string zip { get; set; }
		public string phone { get; set; }
		public string email { get; set; }
		public string password { get; set; }
		public DateTime date_of_birth { get; set; }
		public string person_type { get; set; }

		public Store store { get; set; }
		public ContactPreference contact_preference { get; set; }
	}
}