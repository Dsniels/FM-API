using Core.Entities;
using Core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IGenericRepository<T> where T : Base
    {
        Task<T> getByID(int id);
        Task<IReadOnlyCollection<T>> getAllAsync();
        Task<IReadOnlyCollection<T>> getAllWithSpec(ISpecification<T> spec);
        Task<int> CountAsync(ISpecification<T> specification);

        Task<int> add(T entity);
        Task<int> update(T entitie);
        void AddEntity(T entity);
        void UpdateEntity(T entity);
        Task<int> DeleteEntity(T entity);    

    }
}

