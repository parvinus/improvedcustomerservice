using System;
using System.Collections.Generic;
using ImprovedCustomerService.Data.Dto;

namespace ImprovedCustomerService.Data.Repository
{
    public interface IRepository<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> GetAll();

        TEntity GetById(int customerId);

        int Remove(int customerId);

        void Create(CustomerSaveDto customerSaveDto);

        void Update(CustomerSaveDto customerDto);
    }
}
