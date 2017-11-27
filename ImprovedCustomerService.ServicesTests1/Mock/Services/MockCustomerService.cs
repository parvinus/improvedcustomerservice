using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImprovedCustomerService.Core.Handlers;
using ImprovedCustomerService.Data.Dto;
using ImprovedCustomerService.Data.Validation;
using ImprovedCustomerService.Services.CustomerService;

namespace ImprovedCustomerService.ServicesTests.Mock.Services
{
    public class MockCustomerService : ICustomerService
    {
        public ResponseModel GetById(int customerId)
        {
            return new ResponseModel{Message = "success"};
        }

        public ResponseModel Remove(int customerId)
        {
            return new ResponseModel { Message = "success" };
        }

        public ResponseModel Create(CustomerSaveDto customer)
        {
            //var validator = new Validat();
           // validator.Validate(customer);

            throw new NotImplementedException();
        }

        public ResponseModel Update(CustomerSaveDto customer)
        {
            throw new NotImplementedException();
        }
    }
}
