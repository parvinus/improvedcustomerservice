using FluentValidation.Attributes;
using ImprovedCustomerService.Data.Validation;

namespace ImprovedCustomerService.Data.Dto.Contacts
{
    [Validator(typeof(ContactSaveDtoValidator))]
    public class ContactSaveDto
    {
        public int? ContactId { get; set; }
        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsPrimary { get; set; }
        public string PrimaryPhone { get; set; }
        public string AlternatePhone { get; set; }
        public string EmailAddress { get; set; }
    }
}
