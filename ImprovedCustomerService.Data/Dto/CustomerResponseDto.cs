﻿namespace ImprovedCustomerService.Data.Dto
{
    public class CustomerResponseDto
    {
        public int Id { get; set; }
        public int Age { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public virtual AddressDto Address { get; set; }
    }
}
