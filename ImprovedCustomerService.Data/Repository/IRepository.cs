using System;
using System.Collections.Generic;
using ImprovedCustomerService.Data.Dto;

namespace ImprovedCustomerService.Data.Repository
{
    public interface IRepository : IDisposable
    {
        IList<CustomerResponseDto> GetAll();
    }
}
