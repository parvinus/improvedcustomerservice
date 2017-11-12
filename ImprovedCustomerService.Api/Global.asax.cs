using System.Web.Http;
using ImprovedCustomerService.Data.Configuration;

namespace ImprovedCustomerService.Api
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            AutomapperConfiguration.Configure();
        }
    }
}
