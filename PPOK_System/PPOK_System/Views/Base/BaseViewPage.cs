using System.Web.Mvc;
using PPOK_System.Service.Models;

namespace PPOK_System.Views.Base {
	public abstract class BaseViewPage : WebViewPage {
		public new UserPrincipal User {
			get { return base.User as UserPrincipal; }
		}
	}
	public abstract class BaseViewPage<TModel> : WebViewPage<TModel> {
		public new UserPrincipal User {
			get { return base.User as UserPrincipal; }
		}
	}
}
