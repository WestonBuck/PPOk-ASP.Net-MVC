using System.Configuration;

namespace PPOK_System.Models {
	public class SystemContext {
		static SystemContext() {
			MasterConnectionString = ConfigurationManager.ConnectionStrings["MasterConnection"].ConnectionString;
			DefaultConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
		}

		public static string MasterConnectionString { get; }
		public static string DefaultConnectionString { get; }
	}
}
