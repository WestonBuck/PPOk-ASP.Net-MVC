using PPOK_System.Domain.Service.Cryptography;

namespace PPOK_System.Service.Authentication {
	public static class Password {
		public static bool Authenticate(string attempt, string actual) {
			var encoded = Encrypt.Encode(attempt);
			return encoded == actual ? true : false;
		}
	}
}