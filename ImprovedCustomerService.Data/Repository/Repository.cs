using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using AutoMapper;
using ImprovedCustomerService.Data.Dto;
using ImprovedCustomerService.Data.Model;

namespace ImprovedCustomerService.Data.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : Customer
    {
        private readonly CustomerServiceDbEntities _context;
        public readonly DbSet<TEntity> _DbSet;

        public Repository( CustomerServiceDbEntities context)
        {
            _context = context;
            _DbSet = context.Set<TEntity>();
        }

        public IEnumerable<TEntity> GetAll()
        {
                return Mapper.Map<IEnumerable<TEntity>>(_context.Customers.AsEnumerable());
        }

        public TEntity GetById(int customerId)
        {
            return _DbSet.Find(customerId);
        }

        public int Remove(int customerId)
        {
            var targetEntity = GetById(customerId);
            _DbSet.Remove(targetEntity);

            return _context.SaveChanges();
        }

        public void Create(CustomerSaveDto customerSaveDto)
        {
            _DbSet.Add(Mapper.Map<TEntity>(customerSaveDto));
            _context.SaveChanges();
        }

        public void Update(CustomerSaveDto customerDto)
        {
            var entityToUpdate = _DbSet.Find(customerDto.Id);

            if (entityToUpdate == null)
                return;

            var previousAddressId = entityToUpdate.Address_Id;

            entityToUpdate.Age = customerDto.Age;
            entityToUpdate.Email = customerDto.Email;
            entityToUpdate.FirstName = customerDto.FirstName;
            entityToUpdate.LastName = customerDto.LastName;

            if (customerDto.Address == null)
            {
                entityToUpdate.Address_Id = null;
                entityToUpdate.Address = null;

                var addressToRemove = _context.Addresses.Single(a => a.Id == previousAddressId);
                _context.Addresses.Remove(addressToRemove);
                _context.Entry(addressToRemove).State = EntityState.Deleted;
            }
            else
            {
                if (entityToUpdate.Address == null)
                    entityToUpdate.Address = _context.Addresses.Create();

                entityToUpdate.Address.City = customerDto.Address.City;
                entityToUpdate.Address.PostalCode = customerDto.Address.PostalCode;
                entityToUpdate.Address.State = customerDto.Address.State;
                entityToUpdate.Address.Street = customerDto.Address.Street;
                entityToUpdate.Address.Unit = customerDto.Address.Unit;
            }

            _context.Entry(entityToUpdate).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}