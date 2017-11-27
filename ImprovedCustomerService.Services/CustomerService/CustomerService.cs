using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using AutoMapper;
using ImprovedCustomerService.Core.Handlers;
using ImprovedCustomerService.Data.Dto;
using ImprovedCustomerService.Data.Model;
using ImprovedCustomerService.Data.Repository;

namespace ImprovedCustomerService.Services.CustomerService
{
    [SuppressMessage("ReSharper", "StringIndexOfIsCultureSpecific.1")]
    public class CustomerService : ICustomerService
    {
        #region fields
        private readonly IRepository<Customer> _repository;
        #endregion

        #region constructor(s)
        public CustomerService(IRepository<Customer> repository)
        {
            _repository = repository;
        }
        #endregion

        #region methods

        /// <summary>
        ///     accepting search values for city and state, this method attempts to find a customer whose address(es) match(es)
        ///     the state search string exactly and the city search string at least partially.  Order the results so that full matches are
        ///     first, and then matches are sorted by index of first match.  The sort is ascending.
        /// </summary>
        /// <param name="searchCity">a string containing a full or partial match to an address city</param>
        /// <param name="searchState">a string containing a full match to an address state</param>
        /// <returns></returns>
        public ResponseModel GetByAddressSearch(string searchCity, string searchState)
        {
            /* create empty response model, we will return this no matter the outcome of the search */
            var responseModel = new ResponseModel(new List<string>());

            /*  attempt to retrieve customers whose addresses fully match the state provided and at least partially match the city.
                sort the results to include full matches first, then by index of first match on the city. */
            var result = _repository.Get(c => c.Address.State == searchState && c.Address.City.IndexOf(searchCity) >= 0)
                .OrderBy(c => c.Address.City.IndexOf(searchCity));

            /* check if search results were found and populate the response model accordingly */
            if (result.Any())
            {
                responseModel.Result = Mapper.Map<IEnumerable<CustomerResponseDto>>(result);
                responseModel.Message = "success";
            }
            else
            {
                responseModel.Message = "no matches found";
            }

            return responseModel;
        }

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

        /// <summary>
        ///     attempts to remove from the database a customer matching the given id.
        /// </summary>
        /// <param name="customerId">a numeric id matching the customer id to delete</param>
        /// <returns>a response model containing the result of the transaction</returns>
        public ResponseModel Remove(int customerId)
        {
            /* attempt to remove the customer matching a given id */
            _repository.Remove(customerId);

            /* commit the transaction and record the number of affected rows */
            var rowsDeleted = _repository.Save();

            /*check if any rows were deleted and create the response model accordingly */
            return new ResponseModel
            {
                Message = rowsDeleted > 0 ? "success" : "no customer found"
            };
        }

        public ResponseModel Create(CustomerSaveDto customer)
        {
            //TODO: validate model using modelstate

            var responsePayload = new ResponseModel();

            _repository.Create(Mapper.Map<Customer>(customer));
            _repository.Save();

            responsePayload.Message = "success";

            return responsePayload;
        }

        public ResponseModel Update(CustomerSaveDto customer)
        {
            //TODO: validate model using modelstate

            var response = new ResponseModel();

            //validate the incoming customer payload
            //var customerValidator = new CustomerSaveDtoValidator();
            //var validationResult = customerValidator.Validate(customer);

            _repository.Update(Mapper.Map<Customer>(customer));
            _repository.Save();

            response.Result = null;
            response.Message = "success";

            return response;
        }

        #endregion
    }
}