using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace ImprovedCustomerService.Data.Repository
{
    public class Repository<TEntity> : IRepository<TEntity>, IDisposable where TEntity : class
    {
        private readonly DbContext _context;
        private readonly IDbSet<TEntity> _dbSet;

        public Repository(DbContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public Repository(DbContext context, IDbSet<TEntity> dbSet)
        {
            _context = context;
            _dbSet = dbSet ?? context.Set<TEntity>();
        }

        public Repository(){}

        public TEntity GetById(object id)
        {
            return _dbSet.Find(id);
        }

        public IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> query)
        {
            return _dbSet.Where(query).AsEnumerable();
        }

        public void Remove(int id)
        {
            var targetEntity = GetById(id);
            _dbSet.Remove(targetEntity);
        }

        public void Create(TEntity entityToCreate)
        {
            _dbSet.Add(entityToCreate);
        }

        public void Update(TEntity entityToUpdate)
        {
            if (entityToUpdate == null)
                return;

            _dbSet.Attach(entityToUpdate);
            _context.Entry(entityToUpdate).State = EntityState.Modified;
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