using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ImprovedCustomerService.Data.Repository
{
    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity GetById(object id);

        IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> query);

        void Remove(int id);

        void Create(TEntity entityToCreate);

        void Update(TEntity entityToUpdate);

        int Save();
    }
}
