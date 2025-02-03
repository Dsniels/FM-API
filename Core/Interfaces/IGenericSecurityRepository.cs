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
        Task<T> GetByID(int id);
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<int> Add(T entity);
        Task<int> Update(T entity);

    }
}
