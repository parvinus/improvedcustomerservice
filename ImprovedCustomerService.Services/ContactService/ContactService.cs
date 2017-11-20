using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ImprovedCustomerService.Core.Handlers;
using ImprovedCustomerService.Data.Dto.Contacts;
using ImprovedCustomerService.Data.Model;
using ImprovedCustomerService.Data.Repository;

namespace ImprovedCustomerService.Services.ContactService
{
    public class ContactService : IContactService
    {
        private readonly IRepository<Contact> _repository;

        public ContactService(IRepository<Contact> repository)
        {
            _repository = repository;
        }

        public ResponseModel GetById(int contactId)
        {
            //create a new response model and attempt to get the requested customer from our repo
            var responseModel = new ResponseModel {Errors = new List<string>()};
            var contact = _repository.GetById(contactId);

            //check in the event no match was found and add the appropriate error/message
            if (contact == null)
            {
                responseModel.Errors.Add($"{contactId} does not match an existing contact");
                responseModel.Message = "failed to get contact";
            }

            //add the contact found to the response.  if no contact was found, a null value is still appropriate here.
            responseModel.Result = contact;

            return responseModel;
        }

        public ResponseModel Create(ContactSaveDto contact)
        {
            //convert the incoming contact dto to a usable entity and create a skeleton response model
            var contactEntity = Mapper.Map<Contact>(contact);
            var responseModel = new ResponseModel();

            //attempt to create the contact and add the appropriate message to the response model
            _repository.Create(contactEntity);
            if (_repository.Save() == 0)
            {
                responseModel.Message = "success";
            }
            else responseModel.Errors = new List<string>{"failed to create contact"};

            return responseModel;
        }

        public ResponseModel Remove(int contactId)
        {
            _repository.Remove(contactId);
            var responseModel = new ResponseModel {Message = "success"};

            return responseModel;
        }

        public ResponseModel Update(ContactSaveDto contact)
        {
            var updatedContactEntity = _repository.GetById(contact.ContactId);

            //can't tell from modelstate validation if a contactid was provided in the first place.  uses the same model as create which expects a null
            //contact id.
            if (updatedContactEntity == null)
                return new ResponseModel {Errors = new List<string> {"ContactId is a required field"}};

            //update the values of our retrieved entity.
            updatedContactEntity.AlternatePhone = contact.AlternatePhone;
            updatedContactEntity.EmailAddress = contact.EmailAddress;
            updatedContactEntity.FirstName = contact.FirstName;
            updatedContactEntity.LastName = contact.LastName;
            updatedContactEntity.PrimaryPhone = contact.PrimaryPhone;

            //additionally make sure no other entry associated with this customer is primary
            updatedContactEntity.IsPrimary = contact.IsPrimary;

            //perform the db update
            _repository.Update(updatedContactEntity);

            //create and retur the successful response payload.
            return new ResponseModel {Message = "success"};
        }
    }
}
