using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ImprovedCustomerService.Api.Controllers
{
    [RoutePrefix("api/Contacts")]
    public class ContactsController : ApiController
    {
        [Route("GetAll")]
        public HttpResponseMessage GetAll()
        {
            return Request.CreateResponse(HttpStatusCode.OK, "success");
        }
    }
}
