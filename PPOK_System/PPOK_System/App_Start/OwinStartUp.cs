using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using Hangfire;
using System.Configuration;

[assembly: OwinStartup(typeof(PPOK_System.App_Start.OwinStartUp))]

namespace PPOK_System.App_Start
{
    public class OwinStartUp
    {
        public void Configuration(IAppBuilder app)
        {
            GlobalConfiguration.Configuration.UseSqlServerStorage(@"Data Source=.\SQLEXPRESS;Initial Catalog=PPOk;Integrated Security=True");

            app.UseHangfireDashboard();
            app.UseHangfireServer();
        }
    }
}
