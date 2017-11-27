using System.Net;
using System.Net.Http;
using System.Web.Http;
using ImprovedCustomerService.Data.Dto;
using ImprovedCustomerService.Services.CustomerService;

namespace ImprovedCustomerService.Api.Controllers
{
    [RoutePrefix("api/Customer")]
    public class CustomerController : ApiController
    {
        private readonly ICustomerService _service;

        public CustomerController(ICustomerService customerService)
        {
            _service = customerService;
        }

        [Route("GetByAddressSearch")]
        [HttpGet]
        public HttpResponseMessage GetByAddressSearch(string searchCity, string searchState)
        {
            var payload = _service.GetByAddressSearch(searchCity, searchState);

            return Request.CreateResponse(payload.Errors.Count > 0 ? HttpStatusCode.BadRequest : HttpStatusCode.OK,
                payload);
        }

        /// <summary>
        ///     searches the db for the customer matching the given id and returns it if possible
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        [Route("GetById")]
        [HttpGet]
        public HttpResponseMessage GetById(int customerId)
        {
            var payload = _service.GetById(customerId);
            return Request.CreateResponse(HttpStatusCode.OK, payload);
        }

        /// <summary>
        ///     accepts a customer id and deletes its corresponding record from the database.
        ///     also deletes the associated address, if applicable.
        /// </summary>
        /// <param name="customerId">the unique id that identifies a specific customer</param>
        /// <returns>an int represnting the number of records affected.</returns>
        [Route("Remove")]
        [HttpDelete]
        public HttpResponseMessage Remove(int customerId)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _service.Remove(customerId));
        }

        [Route("Create")]
        [HttpPost]
        public HttpResponseMessage Create(CustomerSaveDto customer)
        {
            if (!ModelState.IsValid)
                return Request.CreateResponse();

            var responseModel = _service.Create(customer);
            responseModel.Message = "success";

            return Request.CreateResponse(HttpStatusCode.OK, responseModel);
        }

        [Route("Update")]
        [HttpPut]
        public HttpResponseMessage Update(CustomerSaveDto targetCustomer)
        {
            if (!ModelState.IsValid)
                return Request.CreateResponse();

            var payload = _service.Update(targetCustomer);
            var statusCode = (payload?.Errors?.Count ?? 0) > 0 ? HttpStatusCode.BadRequest : HttpStatusCode.OK;
            return Request.CreateResponse(statusCode, payload);
        }
    }
}
