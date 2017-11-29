using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ImprovedCustomerService.Data.Repository;

namespace ImprovedCustomerService.Tests.MockRepository
{
    public class MockRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        #region internal fields
        private DbSet<TEntity> _repository;
        #endregion

        #region constructor
        public MockRepository(DbSet<TEntity> repository)
        {
            _repository = repository;
        }
        #endregion

        #region methods
        public TEntity GetById(object id)
        {
            return _repository.Find(id);
        }

        public IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> query)
        {
            return _repository.Where(query);
        }

        public void Remove(int id)
        {
            var entityToRemove = _repository.Find(id);

            if (entityToRemove != null)
                _repository.Remove(entityToRemove);
        }

        public void Create(TEntity entityToCreate)
        {
            _repository.Add(entityToCreate);
        }

        public void Update(TEntity entityToUpdate)
        {
            _repository.Remove(entityToUpdate);
            _repository.Add(entityToUpdate);
        }

        public int Save()
        {
            return 1;
        }

        #endregion
    }
}
