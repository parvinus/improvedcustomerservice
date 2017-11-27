using ImprovedCustomerService.Core.Handlers;
using ImprovedCustomerService.Data.Dto.Contacts;

namespace ImprovedCustomerService.Services.ContactService
{
    public interface IContactService
    {
        ResponseModel GetById(int contactId);

        ResponseModel GetBySearch(string searchInput);

        ResponseModel Create(ContactSaveDto contact);

        ResponseModel Remove(int contactId);

        ResponseModel Update(ContactSaveDto contact);
    }
}
