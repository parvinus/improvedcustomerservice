using System.Web.Http;
using FluentValidation.WebApi;
using ImprovedCustomerService.Core.Filters;

namespace ImprovedCustomerService.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API routes
            config.MapHttpAttributeRoutes();

            // Web API configuration and services
            config.Filters.Add(new ValidateModelStateFilter());
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            /* wire fluent to ModelState validation */
        }
    }
}
