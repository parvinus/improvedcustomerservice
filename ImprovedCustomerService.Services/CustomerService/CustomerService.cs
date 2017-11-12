using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using ImprovedCustomerService.Core.Handlers;
using ImprovedCustomerService.Core.Utility;
using ImprovedCustomerService.Data.Dto;
using ImprovedCustomerService.Data.Repository;
using ImprovedCustomerService.Data.Validation;

namespace ImprovedCustomerService.Services.CustomerService
{
    public class CustomerService
    {
        private readonly HttpRequestMessage _request;
        private readonly CustomerRepository _repository;

        public CustomerService(HttpRequestMessage request)
        {
            _request = request;
            _repository = new CustomerRepository();
        }

        public HttpResponseMessage GetAll()
        {
            return ResponseHandler.GetResponse(_request, HttpStatusCode.OK, "", new List<string>(),
                _repository.GetAll());
        }

        public HttpResponseMessage GetById(int customerId)
        {
            var customer = _repository.GetById(customerId);
            var message = customer == null ? $"no customer matching {customerId} was found" : "success";

            return ResponseHandler.GetResponse(_request, HttpStatusCode.OK, message, null, customer);
        }

        public HttpResponseMessage Remove(int customerId)
        {
            var recordsRemoved = _repository.Remove(customerId);
            var message = recordsRemoved > 0 ? "success" : "requested customer was not found.";

            return ResponseHandler.GetResponse(_request, HttpStatusCode.OK, message);
        }

        public HttpResponseMessage Create(CustomerSaveDto customer)
        {
            var customerValidator = new CustomerSaveDtoValidator();
            var validationResult = customerValidator.Validate(customer);

            var rowsAdded = 0;

            if (validationResult?.IsValid == true)
                rowsAdded = _repository.Create(customer);
            var message = rowsAdded > 0 ? "success" : "failed to create customer";
            return ResponseHandler.GetResponse(_request, HttpStatusCode.OK, message);

        }

        public HttpResponseMessage Update(CustomerSaveDto customer)
        {
            var response = new ResponseHandler(_request);

            //validate the incoming customer payload
            var customerValidator = new CustomerSaveDtoValidator();
            var validationResult = customerValidator.Validate(customer);

            if (!validationResult.IsValid)
            {
                response.Errors = TypeConversion.ValidationFailureToString(validationResult.Errors);
                response.Message = "failed to update";
                return response.CreateResponse;
            }

            object result = _repository.Update(customer);
            response.Payload = result;
            response.Message = result != null ? "success" : "no customers were updated.";

            return response.CreateResponse;
        }
    }
}