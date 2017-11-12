using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using AutoMapper;
using ImprovedCustomerService.Data.Dto;
using ImprovedCustomerService.Data.Model;

namespace ImprovedCustomerService.Data.Repository
{
    public class CustomerRepository : IRepository
    {
        private readonly CustomerServiceDbEntities _context;

        public CustomerRepository()
        {
            _context = new CustomerServiceDbEntities();
        }

        /// <inheritdoc />
        /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        public void Dispose()
        {

        }

        public IList<CustomerResponseDto> GetAll()
        {
            var customerList = _context.Customers.AsEnumerable();
            return Mapper.Map<IList<CustomerResponseDto>>(customerList);
        }

        public CustomerResponseDto GetById(int customerId)
        {
            using (var db = new CustomerServiceDbEntities())
            {
                var customer = db.Customers.SingleOrDefault(c => c.Id == customerId);
                var customerDto = Mapper.Map<CustomerResponseDto>(customer);

                return customerDto;
            }
        }

        private Customer GetCustomerById(int customerId)
        {
            return _context.Customers?.Single(c => c.Id == customerId);
        }

        public int Remove(int customerId)
        {
            var target = GetCustomerById(customerId);
            var address = target.Address;
            _context.Addresses.Remove(address);
            _context.Customers.Remove(target);
            return _context.SaveChanges();

            //using (var db = new CustomerServiceDbEntities())
            //{
            //    var customer = db.Customers.SingleOrDefault(c => c.Id == customerId);

            //    if (customer != null)
            //    {
            //        db.Customers.Remove(customer);
            //        return db.SaveChanges();
            //    }
            //}
            //return 0;
        }

        public int Create(CustomerSaveDto customerDto)
        {
            var customerDbo = Mapper.Map<Customer>(customerDto);
            _context.Customers.Add(customerDbo);
            return _context.SaveChanges();
        }

        /// <summary>
        ///     given a CustomerSaveDto, attempts to update the database.
        /// </summary>
        /// <param name="customerSaveDto"></param>
        /// <returns>
        ///     returns a responseDto containing the
        /// </returns>
        public CustomerResponseDto Update(CustomerSaveDto customerSaveDto)
        {
            /*  1, attempt to save the customer to the database.
                2,  reformat the result into a response-friendly Dto and return it  */

            /* check if address info is present.  If no address payload was provided, we need
                 to remove any previously associated address.  (we want to prevent orphaned data)*/

            Customer customerDbo = null;

            if (customerSaveDto.Id != null)
            {
                customerDbo = GetCustomerById(customerSaveDto.Id.Value);
                var previousAddressId = customerDbo.Address_Id;

                customerDbo.Age = customerSaveDto.Age;
                customerDbo.Email = customerSaveDto.Email;
                customerDbo.FirstName = customerSaveDto.FirstName;
                customerDbo.LastName = customerSaveDto.LastName;

                if (customerSaveDto.Address == null)
                {
                    customerDbo.Address_Id = null;
                    customerDbo.Address = null;

                    var addressToRemove = _context.Addresses.Single(a => a.Id == previousAddressId);
                    _context.Addresses.Remove(addressToRemove);
                    _context.Entry(addressToRemove).State = EntityState.Deleted;
                }
                else
                {
                    if (customerDbo.Address == null)
                        customerDbo.Address = _context.Addresses.Create();

                    customerDbo.Address.City = customerSaveDto.Address.City;
                    customerDbo.Address.PostalCode = customerSaveDto.Address.PostalCode;
                    customerDbo.Address.State = customerSaveDto.Address.State;
                    customerDbo.Address.Street = customerSaveDto.Address.Street;
                    customerDbo.Address.Unit = customerSaveDto.Address.Unit;
                }

                _context.Entry(customerDbo).State = EntityState.Modified;
                _context.SaveChanges();
            }

            return Mapper.Map<CustomerResponseDto>(customerDbo);
        }
    }
}
