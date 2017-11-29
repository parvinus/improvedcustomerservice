using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using ImprovedCustomerService.Core.Handlers;

namespace ImprovedCustomerService.Core.Filters
{
    public class ValidateModelStateFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            /* no need to do anything if the modelstate is valid for this context */
            if (actionContext.ModelState.IsValid) return;

            /* at this point the state is invalid.  get the list of errors from modelstate and convert it to a list of strings */
            var response = new ResponseModel
            {
                Message = "invalid request",
                Errors = actionContext.ModelState.Values.SelectMany(m => m.Errors.Select(e => e.ErrorMessage)).ToList()
            };

            /* set up the response */
            actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.BadRequest, response);
        }
    }
}
