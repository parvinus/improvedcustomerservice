using System.Web.Http;
using FluentValidation.WebApi;
using ImprovedCustomerService.Data.Configuration;

namespace ImprovedCustomerService.Api
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AutomapperConfiguration.Configure();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FluentValidationModelValidatorProvider.Configure(GlobalConfiguration.Configuration);
        }
    }
}
