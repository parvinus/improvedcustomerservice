using FluentValidation;
using ImprovedCustomerService.Data.Dto.Contacts;
using ImprovedCustomerService.Data.Model;
using ImprovedCustomerService.Data.Repository;

namespace ImprovedCustomerService.Data.Validation
{
    public class ContactSaveDtoValidator : AbstractValidator<ContactSaveDto>
    {
        public ContactSaveDtoValidator()
        {
            //ensure a contact was actually passed to the service.
            RuleFor(c => c).NotEmpty().WithMessage("Contact data is required.");

            RuleFor(c => c.LastName).NotEmpty().WithMessage("Last name is a required field.");

            RuleFor(c => c.EmailAddress).NotEmpty().EmailAddress();

            RuleFor(c => c.ContactId).Must(validateContactId).WithMessage("invalid contact id").When(c => c.ContactId != null);

            RuleFor(c => c.CustomerId).Must(validateCustomerId).WithMessage("customer id doesn't exist");
        }

        private bool validateCustomerId(int id)
        {
            using (var db = new CustomerServiceDbEntities())
            {
                return db.Customers.Find(id) != null;
            }
        }

        private bool validateContactId(int? id)
        {
            using (var db = new CustomerServiceDbEntities())
            {
                return db.Contacts.Find(id) != null;
            }
        }
    }
}
