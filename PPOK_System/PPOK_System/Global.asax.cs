using PPOK_System.Service.Models;
using System;
using System.Diagnostics;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Script.Serialization;
using System.Web.Security;

namespace PPOK_System
{
    public class MvcApplication : HttpApplication {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

		protected void Application_PostAuthenticateRequest(Object sender, EventArgs e) {
			if (FormsAuthentication.CookiesSupported == true) {
				if (Request.Cookies[FormsAuthentication.FormsCookieName] != null) {
					HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
					Debug.WriteLine(authCookie.Value);

					if (authCookie != null) {
						try {
							FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);
							JavaScriptSerializer serializer = new JavaScriptSerializer();
							UserPrincipalSerialize serializeModel = serializer.Deserialize<UserPrincipalSerialize>(authTicket.UserData);
							UserPrincipal newUser = new UserPrincipal(serializeModel);
							Debug.WriteLine(authTicket.Name);

							HttpContext.Current.User = newUser;
						} catch (Exception exception) {
							Console.WriteLine(exception.Message);
						}
					}
				}
			}
		}
	}
}
