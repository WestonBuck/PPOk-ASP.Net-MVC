using System;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Security;
using PPOK_System.Service.Models;

namespace PPOK_System.Service.Authentication {
	public static class AuthTicket {
		public static HttpCookie Make(UserPrincipalSerialize user) {
			// clear any current sign in cookies
			FormsAuthentication.SignOut();
			JavaScriptSerializer serializer = new JavaScriptSerializer();
			user.Store.pharmacists = null;      // stop any circular references
			string userData = serializer.Serialize(user);

			FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(1, user.Email, DateTime.Now, DateTime.Now.AddMinutes(30), false, userData);
			string encTicket = FormsAuthentication.Encrypt(authTicket);
			return new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
		}
	}
}