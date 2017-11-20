using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using AutoMapper;
using ImprovedCustomerService.Data.Dto;
using ImprovedCustomerService.Data.Model;

namespace ImprovedCustomerService.Data.Repository
{
    public class Repository<TEntity> : IRepository<TEntity>, IDisposable where TEntity : class
    {
        private readonly CustomerServiceDbEntities _context;

        public Repository(CustomerServiceDbEntities context)
        {
            _context = context;
        }

        public TEntity GetById(object id)
        {
            return _context.Set<TEntity>().Find(id);
        }

        public void Remove(int id)
        {
            var targetEntity = GetById(id);
            _context.Set<TEntity>().Remove(targetEntity);

            _context.SaveChanges();
        }

        public void Create(TEntity entityToCreate)
        {
            _context.Set<TEntity>().Add(entityToCreate);
            _context.SaveChanges();
        }

        public void Update(TEntity entityToUpdate)
        {
            if (entityToUpdate == null)
                return;

            var dbSet = _context.Set<TEntity>();
            dbSet.Attach(entityToUpdate);

            _context.Entry(entityToUpdate).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public int Save()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
        }
    }
}