using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IGenericSecurityRepository<T> where T : IdentityUser
    {
        Task<T> GetByIDAsync(int id);
        Task<IReadOnlyCollection<T>> GetAllAsync();
        Task<int> add(T entity);
        Task<int> update(T entity);

    }
}
