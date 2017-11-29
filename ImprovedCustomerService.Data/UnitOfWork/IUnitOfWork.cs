using System;
using ImprovedCustomerService.Data.Model;
using ImprovedCustomerService.Data.Repository;

namespace ImprovedCustomerService.Data.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        #region properties

        IRepository<Customer> CustomerRepository { get; }

        IRepository<Contact> ContactRepository { get; }

        #endregion

        #region methods

        int SaveChanges();

        #endregion
    }
}
