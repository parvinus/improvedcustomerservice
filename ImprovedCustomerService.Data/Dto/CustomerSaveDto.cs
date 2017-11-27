using FluentValidation.Attributes;
using ImprovedCustomerService.Data.Validation;

namespace ImprovedCustomerService.Data.Dto
{
    [Validator(typeof(CustomerSaveDtoValidator))]
    public sealed class CustomerSaveDto
    {
        public int? Id { get; set; }
        public int Age { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int? AddressId { get; set; }

        public AddressDto Address { get; set; }
    }
}
