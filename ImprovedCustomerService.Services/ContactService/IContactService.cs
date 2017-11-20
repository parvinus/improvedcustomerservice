using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImprovedCustomerService.Core.Handlers;
using ImprovedCustomerService.Data.Dto.Contacts;
using ImprovedCustomerService.Data.Model;

namespace ImprovedCustomerService.Services.ContactService
{
    public interface IContactService
    {
        ResponseModel GetById(int contactId);

        ResponseModel Create(ContactSaveDto contact);

        ResponseModel Remove(int contactId);

        ResponseModel Update(ContactSaveDto contact);
    }
}
