using System.Net.Http;
using System.Web.Http;
using ImprovedCustomerService.Data.Dto;
using ImprovedCustomerService.Services.CustomerService;

namespace ImprovedCustomerService.Api.Controllers
{
    [RoutePrefix("api/Customer")]
    public class CustomerController : ApiController
    {
        private CustomerService _service;
        private CustomerService CustomerService => _service ?? (_service = new CustomerService(Request));

        [Route("GetAll")]
        [HttpGet]
        public HttpResponseMessage GetAll()
        {
            return CustomerService.GetAll();
        }

        [Route("GetById")]
        [HttpGet]
        public HttpResponseMessage GetById(int customerId)
        {
            return CustomerService.GetById(customerId);
        }

        [Route("Remove")]
        [HttpDelete]
        public HttpResponseMessage Remove(int customerId)
        {
            return CustomerService.Remove(customerId);
        }

        [Route("Create")]
        [HttpPost]
        public HttpResponseMessage Create(CustomerSaveDto customer)
        {
            return CustomerService.Create(customer);
        }

        [Route("Update")]
        [HttpPut]
        public HttpResponseMessage Update(CustomerSaveDto targetCustomer)
        {
            return CustomerService.Update(targetCustomer);
        }
    }
}
