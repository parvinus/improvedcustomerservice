using System.Collections.Generic;
using ImprovedCustomerService.Data.Dto;

namespace ImprovedCustomerService.Data.Repository
{
    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity GetById(object id);

        void Remove(int id);

        void Create(TEntity entityToCreate);

        void Update(TEntity entityToUpdate);

        int Save();
    }
}
