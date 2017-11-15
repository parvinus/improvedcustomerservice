using System.Collections.Generic;
using System.Net.Http;
using ImprovedCustomerService.Core.Handlers;
using ImprovedCustomerService.Data.Dto;
using ImprovedCustomerService.Data.Model;

namespace ImprovedCustomerService.Services.CustomerService
{
    public interface ICustomerService
    {
        ResponseModel GetAll();

        ResponseModel GetById(int customerId);

        ResponseModel Remove(int customerId);

        ResponseModel Create(CustomerSaveDto customer);

        ResponseModel Update(CustomerSaveDto customer);
    }
}
