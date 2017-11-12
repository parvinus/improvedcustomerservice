using FluentValidation;
using ImprovedCustomerService.Data.Dto;
using ImprovedCustomerService.Data.Repository;

namespace ImprovedCustomerService.Data.Validation
{
    public class CustomerSaveDtoValidator : AbstractValidator<CustomerSaveDto>
    {
        public CustomerSaveDtoValidator()
        {
            //ensure a customer was actually passed to the service.
            RuleFor(c => c).NotEmpty().WithMessage("A customer payload is required.");

            //if the customer id provided is not null, it must exist in the database.
            //a null id is allowed in the case of creating new customers.
            RuleFor(c => c.Id).Must(ValidateId).When(c => c?.Id != null).WithMessage("customer doesn't exist with that Id");

            //an age is required and must be within the range 10-20
            RuleFor(c => c.Age).NotEmpty().GreaterThan(9).LessThan(21).WithMessage("age is required.");

            RuleFor(c => c.FirstName).Length(3, 50);
            RuleFor(c => c.Email).NotEmpty().EmailAddress();
            RuleFor(c => c.Address)?.SetValidator(new AddressDtoValidator()).WithMessage("address is invalid").When(c => c?.Address != null);

        }

        private bool ValidateId(int? customerId)
        {
            using (var repo = new CustomerRepository())
            {
                return customerId != null && repo.GetById(customerId.Value) != null;
            }
        }
    }
}
