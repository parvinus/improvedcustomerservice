using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using AutoMapper;
using ImprovedCustomerService.Core.Handlers;
using ImprovedCustomerService.Data.Dto.Contacts;
using ImprovedCustomerService.Data.Model;
using ImprovedCustomerService.Data.Repository;
using ImprovedCustomerService.Data.UnitOfWork;

namespace ImprovedCustomerService.Services.ContactService
{
    [SuppressMessage("ReSharper", "StringIndexOfIsCultureSpecific.1")]
    public class ContactService : IContactService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ContactService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ResponseModel GetById(int contactId)
        {
            //create a new response model and attempt to get the requested customer from our repo
            var responseModel = new ResponseModel();
            var contact = _unitOfWork.ContactRepository.GetById(contactId);
            //var contact = _repository.GetById(contactId);

            //check in the event no match was found and add the appropriate error/message
            if (contact == null)
            {
                responseModel.Errors = new List<string> {$"{contactId} does not match an existing contact"};
                responseModel.Message = "failed to get contact";
            }
            else
            {
                //add the contact found to the response.  if no contact was found, a null value is still appropriate here.
                responseModel.Result = contact;
                responseModel.Message = "success";
            }

            return responseModel;
        }

        public ResponseModel GetBySearch(string searchInput)
        {
            /* create an empty response model, we'll use it no matter what */
            var responsePayload = new ResponseModel(new List<string>());

            /* attempt to get matches for the search input, as  well as sort the search results.*/
            var payload = _unitOfWork.ContactRepository.Get(c =>
                c.FirstName.IndexOf(searchInput) > -1 ||
                c.LastName.IndexOf(searchInput) > -1 ||
                c.EmailAddress.IndexOf(searchInput) > -1)
                .OrderBy(c => c.FirstName.IndexOf(searchInput))
                .ThenBy(c => c.LastName.IndexOf(searchInput))
                .ThenBy(c => c.EmailAddress.IndexOf(searchInput));

            /* assign the appropriate response message and add the search results payload */
            responsePayload.Message = payload.Any() ? "success" : "no results found";
            responsePayload.Result = payload;

            return responsePayload;
        }

        public ResponseModel Create(ContactSaveDto contact)
        {
            var responseModel = new ResponseModel{Errors = new List<string>()};

            //check for a provided contact id, which is invalid.  new contacts cannot have an id specified.
            if (contact.ContactId != null)
                responseModel.Errors.Add("ContactId must be null when creating a new contact");

            //validate the customer id provided.  a contact must be assigned to a valid customer.
            if (_unitOfWork.CustomerRepository.GetById(contact.CustomerId) == null)
                responseModel.Errors.Add("customer id does not exist");

            if (responseModel.Errors.Count == 0)
            {
                //convert the incoming contact dto to a usable entity and create a skeleton response model
                var contactEntity = Mapper.Map<Contact>(contact);

                //attempt to create the contact and add the appropriate message to the response model
                _unitOfWork.ContactRepository.Create(contactEntity);

                //reset, if necessary, other contacts' IsPrimary flags
                if(contactEntity.IsPrimary)
                    UnsetPrimaryContact(contactEntity.ContactId, contactEntity.CustomerId);

                if (_unitOfWork.ContactRepository.Save() > 0)
                    responseModel.Message = "success";
                else responseModel.Errors.Add("failed to create contact");
            }

            return responseModel;
        }

        public ResponseModel Remove(int contactId)
        {
            _unitOfWork.ContactRepository.Remove(contactId);
            var result = _unitOfWork.ContactRepository.Save();
            var responseModel = new ResponseModel();

            if (result > 0)
            {
                responseModel.Message = "success";
            }
            else
            {
                responseModel.Errors = new List<string>{"contact id provided doesn't exist"};
                responseModel.Message = "failed to remove contact";
            }

            return responseModel;
        }

        public ResponseModel Update(ContactSaveDto contact)
        {
            var updatedContactEntity = _unitOfWork.ContactRepository.GetById(contact.ContactId);

            //can't tell from modelstate validation if a contactid was provided in the first place.  uses the same model as create which expects a null
            //contact id.
            if (updatedContactEntity == null)
                return new ResponseModel {Errors = new List<string> {"ContactId provided was invalid"}};

            if(_unitOfWork.CustomerRepository.GetById(contact.CustomerId) == null)
                return new ResponseModel { Errors = new List<string> { "CustomerId provided was invalid" } };

            //update the values of our retrieved entity.
            updatedContactEntity.AlternatePhone = contact.AlternatePhone ?? "";
            updatedContactEntity.EmailAddress = contact.EmailAddress;
            updatedContactEntity.FirstName = contact.FirstName;
            updatedContactEntity.LastName = contact.LastName;
            updatedContactEntity.PrimaryPhone = contact.PrimaryPhone;

            //we need to reset other primary contacts (potentially) if the assigned customer changes or the contact wasn't previously primary
            //but only if we are currently being set to primary.
            if ((!updatedContactEntity.IsPrimary || updatedContactEntity.CustomerId != contact.CustomerId) && contact.IsPrimary)
                UnsetPrimaryContact(contact.ContactId, contact.CustomerId);

            updatedContactEntity.IsPrimary = contact.IsPrimary;
            updatedContactEntity.CustomerId = contact.CustomerId;

            //perform the db update
            _unitOfWork.ContactRepository.Update(updatedContactEntity);
            var result = _unitOfWork.ContactRepository.Save();

            //create and return the successful response payload.
            return new ResponseModel {Message = result > 0 ? "success" : "no records updated."};
        }

        private void UnsetPrimaryContact(int? newPrimaryContactId, int customerId)
        {
            //look up the contact matching the given customer id and NOT matching the given contact id and where IsPrimary is set to true
            var contactToUpdate = _unitOfWork.ContactRepository.Get(c => c.ContactId != newPrimaryContactId && c.CustomerId == customerId && c.IsPrimary).SingleOrDefault();

            //check if a contact was found to update
            if (contactToUpdate == null)
                return;

            //update the contact to not be the primary contact.
            contactToUpdate.IsPrimary = false;
            _unitOfWork.ContactRepository.Update(contactToUpdate);

            //intentionally not saving the transaction here to avoid accidental commit of data changed before this method is called.
        }
    }
}
