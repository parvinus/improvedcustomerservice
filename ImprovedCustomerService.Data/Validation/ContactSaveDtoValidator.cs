using FluentValidation;
using ImprovedCustomerService.Data.Dto.Contacts;

namespace ImprovedCustomerService.Data.Validation
{
    public class ContactSaveDtoValidator : AbstractValidator<ContactSaveDto>
    {
        public ContactSaveDtoValidator()
        {
            //ensure a contact was actually passed to the service.
            RuleFor(c => c).NotEmpty().WithMessage("Contact data is required.");

            RuleFor(c => c.LastName).NotEmpty().WithMessage("Last name is a required field.");

            RuleFor(c => c.EmailAddress).NotEmpty().WithMessage("Email Address is a required field")
                .EmailAddress().WithMessage("Email Address provided is invalid");
        }
    }
}
