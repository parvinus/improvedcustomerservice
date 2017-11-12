using FluentValidation;
using ImprovedCustomerService.Data.Dto;

namespace ImprovedCustomerService.Data.Validation
{
    class AddressDtoValidator : AbstractValidator<AddressDto>
    {
        public AddressDtoValidator()
        {
            //RuleFor(a => a.Id).Must(IsAddressValid).WithMessage("address doesn't exist with that Id")
            //    .When(a => a?.Id != null);
            RuleFor(a => a.Street).NotEmpty().WithMessage("address street is required.");
            RuleFor(a => a.City).NotEmpty().WithMessage("address city is required.");
            RuleFor(a => a.PostalCode).NotNull().Length(3, 10).WithMessage("invalid postal code.  must be between 3 and 10 characters");
        }

        //private bool IsAddressValid(int? arg)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
