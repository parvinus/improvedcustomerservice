using ImprovedCustomerService.Services.ContactService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ImprovedCustomerService.Data.Dto.Contacts;

namespace ImprovedCustomerService.Api.Controllers
{
    [RoutePrefix("api/Contacts")]
    public class ContactsController : ApiController
    {
        private readonly IContactService _contactService;

        public ContactsController(IContactService contactService)
        {
            _contactService = contactService;
        }

        [Route("GetById")]
        [HttpGet]
        public HttpResponseMessage GetById(int contactId)
        {
            var payload = _contactService.GetById(contactId);
            return Request.CreateResponse(HttpStatusCode.OK, payload);
        }

        [Route("Create")]
        [HttpPost]
        public HttpResponseMessage Create(ContactSaveDto contactDto)
        {
            //validate the incoming Dto
            if (!ModelState.IsValid)
                return Request.CreateResponse();

            var payload = _contactService.Create(contactDto);
            return Request.CreateResponse(HttpStatusCode.OK, payload);
        }

        [Route("Remove")]
        [HttpDelete]
        public HttpResponseMessage Remove(int contactId)
        {
            var payload = _contactService.Remove(contactId);
            return Request.CreateResponse(HttpStatusCode.OK, payload);
        }

        [Route("Update")]
        [HttpPut]
        public HttpResponseMessage Update(ContactSaveDto updatedContactDto)
        {
            if (!ModelState.IsValid)
                return Request.CreateResponse();

            var responseModel = _contactService.Update(updatedContactDto);

            return Request.CreateResponse(responseModel.Errors == null ? HttpStatusCode.OK : HttpStatusCode.BadRequest,
                responseModel);
        }
    }
}
