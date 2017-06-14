using System;

// Represents a single prescription that a given user has
namespace PPOK_System.Domain.Models {
	public class Prescription {
		public int? prescription_id { get; set; }
		public int? person_id { get; set; }
		public string drug_id { get; set; }
		public DateTime date_filled { get; set; }
		public int? days_supply { get; set; }
		public int? number_refills { get; set; }

		public Person customer { get; set; }
		public Drug drug { get; set; }
	}
}