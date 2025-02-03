using System;
using System.Security.AccessControl;
using BusinessLogic.Persistence;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogic.Logic;

public class GenericSecurityRepository<T> : IGenericSecurityRepository<T> where T : IdentityUser
{
    private readonly SeguridadDbContext _context;

    public GenericSecurityRepository(SeguridadDbContext context){
        _context = context;
    }


    public async Task<int> Add(T entity)
    {
        _context.Set<T>().Add(entity);
        return await _context.SaveChangesAsync();
    }

    public async Task<IReadOnlyList<T>> GetAllAsync()
    {

        return await _context.Set<T>().ToListAsync();

    }

    public async Task<T> GetByID(int id)
    {
        return await _context.Set<T>().FindAsync(id);

    }

    public async Task<int> Update(T entity)
    {
        _context.Set<T>().Attach(entity);
        _context.Entry(entity).State = EntityState.Modified;
        return await _context.SaveChangesAsync();
        
    }
}
