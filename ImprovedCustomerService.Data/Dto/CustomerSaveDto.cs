namespace ImprovedCustomerService.Data.Dto
{
    public class CustomerSaveDto
    {
        public int? Id { get; set; }
        public int Age { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int? AddressId { get; set; }

        public virtual AddressDto Address { get; set; }
    }
}
