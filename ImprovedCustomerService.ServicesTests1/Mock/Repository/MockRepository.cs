using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ImprovedCustomerService.Data.Repository;
using Moq;

namespace ImprovedCustomerService.ServicesTests.Mock.Repository
{
    class MockRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly IDbSet<TEntity> _mockEntities;

        public MockRepository(IDbSet<TEntity> mockEntities)
        {
            _mockEntities = mockEntities;
        } 

        public TEntity GetById(object id)
        {
            return _mockEntities.Find(id);
        }

        public IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> query)
        {
            return _mockEntities.Where(query);
        }

        public void Remove(int id)
        {
            var entityToRemove = _mockEntities.Find(id);

            if (entityToRemove != null)
                _mockEntities.Remove(entityToRemove);
        }

        public void Create(TEntity entityToCreate)
        {
            _mockEntities.Add(entityToCreate);
        }

        public void Update(TEntity entityToUpdate)
        {
            _mockEntities.Remove(entityToUpdate);
            _mockEntities.Add(entityToUpdate);
        }

        public int Save()
        {
            return 1;
        }
    }
}
