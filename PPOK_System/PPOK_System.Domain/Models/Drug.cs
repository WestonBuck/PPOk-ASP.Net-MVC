// Represents a drug that will be tied to Prescription(s)
namespace PPOK_System.Domain.Models {
	public class Drug {
		public string drug_id { get; set; }		// NDCUPCHRI
		public string drug_name { get; set; }
	}
}