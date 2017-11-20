using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http.ModelBinding;
using AutoMapper;
using ImprovedCustomerService.Core.Handlers;
using ImprovedCustomerService.Core.Utility;
using ImprovedCustomerService.Data.Dto;
using ImprovedCustomerService.Data.Model;
using ImprovedCustomerService.Data.Repository;
using ImprovedCustomerService.Data.Validation;

namespace ImprovedCustomerService.Services.CustomerService
{
    public class CustomerService : ICustomerService
    {
        #region fields
        private readonly HttpRequestMessage _request;
        private readonly IRepository<Customer> _repository;
        #endregion

        #region constructor(s)
        public CustomerService(HttpRequestMessage request, IRepository<Customer> repository)
        {
            _request = request;
            _repository = repository;
        }
        #endregion

        #region methods
        //public ResponseModel GetAll()
        //{
        //    var allCustomers = Mapper.Map<IEnumerable<CustomerResponseDto>>(_repository.GetAll());
        //    return new ResponseModel {Result = allCustomers};
        //}

        /// <summary>
        ///     gets a customer matching the given id from the database, converts the customer entity to a dto and forms
        ///     a standard response model + returns it.
        /// </summary>
        /// <param name="customerId">int matching the id of a customer in the db</param>
        /// <returns></returns>
        public ResponseModel GetById(int customerId)
        {
            var model = new ResponseModel { Result = Mapper.Map<CustomerResponseDto>(_repository.GetById(customerId)) };

            if (model.Result == null)
            {
                model.Message = "request failed";
                model.Errors = new List<string> { $"no customer with Id {customerId} exists" };
            }

            return model;
        }

        public ResponseModel Remove(int customerId)
        {
            var payload = new ResponseModel{Message = "success"};
            _repository.Remove(customerId);
            _repository.Save();

            return payload;
        }

        public ResponseModel Create(CustomerSaveDto customer)
        {
            //var customerValidator = new CustomerSaveDtoValidator();
            //var validationResult = customerValidator.Validate(customer);
            var responsePayload = new ResponseModel();
            _repository.Create(Mapper.Map<Customer>(customer));
            //var message = rowsAdded > 0 ? "success" : "failed to create customer";

            responsePayload.Message = "success";

            return responsePayload;
        }

        #endregion

        //public HttpResponseMessage Create(CustomerSaveDto customer)
        //{
        //    var customerValidator = new CustomerSaveDtoValidator();
        //    var validationResult = customerValidator.Validate(customer);

        //    var rowsAdded = 0;

        //    if (validationResult?.IsValid == true)
        //        rowsAdded = _repository.Create(customer);
        //    var message = rowsAdded > 0 ? "success" : "failed to create customer";
        //    return new HttpResponseMessage();
        //    //return ResponseHandler.GetResponse(_request, HttpStatusCode.OK, message);

        //}

        public ResponseModel Update(CustomerSaveDto customer)
        {
            var response = new ResponseModel();

            //validate the incoming customer payload
            var customerValidator = new CustomerSaveDtoValidator();
            var validationResult = customerValidator.Validate(customer);

            //if (!validationResult.IsValid)
            //{
            //    response.Errors = TypeConversion.ValidationFailureToString(validationResult.Errors);
            //    response.Message = "failed to update";

            //    return response;
            //}

            _repository.Update(Mapper.Map<Customer>(customer));
            response.Result = null;
            response.Message = "success";

            return response;
        }
    }
}