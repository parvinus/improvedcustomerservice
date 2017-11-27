using ImprovedCustomerService.Core.Handlers;
using ImprovedCustomerService.Data.Dto;

namespace ImprovedCustomerService.Services.CustomerService
{
    public interface ICustomerService
    {
        ResponseModel GetByAddressSearch(string searchCity, string searchState);

        ResponseModel GetById(int customerId);

        ResponseModel Remove(int customerId);

        ResponseModel Create(CustomerSaveDto customer);

        ResponseModel Update(CustomerSaveDto customer);
    }
}
