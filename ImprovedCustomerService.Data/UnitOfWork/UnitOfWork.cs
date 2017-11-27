using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImprovedCustomerService.Data.Model;
using ImprovedCustomerService.Data.Repository;

namespace ImprovedCustomerService.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        #region internal fields
        private readonly CustomerServiceDbEntities _context;
        #endregion

        #region properties
        public IRepository<Customer> CustomerRepository { get; }
        public IRepository<Contact> ContactRepository { get; }
        #endregion

        #region constructor(s)
        public UnitOfWork(CustomerServiceDbEntities context)
        {
            _context = context;

            CustomerRepository = CustomerRepository ?? (CustomerRepository = new Repository<Customer>(context));
            ContactRepository = ContactRepository ?? (ContactRepository = new Repository<Contact>(context));
        }
        #endregion

        #region methods
        /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        public void Dispose()
        {
            _context.Dispose();
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }
        #endregion
    }
}
