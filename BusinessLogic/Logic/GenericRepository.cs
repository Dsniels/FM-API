using BusinessLogic.Data;
using BusinessLogic.Persistence;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Logic
{
    public class GenericRepository<T> : IGenericRepository<T> where T : Base
    {
        private readonly SiaseDbContext _context;

        public GenericRepository(SiaseDbContext context) { 
            
            _context = context;
        }
        public async Task<int> add(T entity)
        {
            _context.Set<T>().Add(entity);
            return await _context.SaveChangesAsync();
        }

        public void AddEntity(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public async Task<int> CountAsync(ISpecification<T> specification)
        {
            return await ApplySpecification(specification).CountAsync();
        }

        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            return SpecificationEvaluator<T>.getQuery(_context.Set<T>().AsQueryable(), spec);
        }


        public async Task<int> DeleteEntity(T entity)
        {
            _context.Set<T>().Remove(entity);
            return await _context.SaveChangesAsync();
        }

        public async Task<IReadOnlyCollection<T>> getAllAsync()
        {
            return await _context.Set<T>().ToListAsync();   
        }

        public async Task<IReadOnlyCollection<T>> getAllWithSpec(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).ToListAsync();
        }

        public async Task<T> getByID(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<int> update(T entitie)
        {
            _context.Set<T>().Attach(entitie);
            _context.Entry(entitie).State = EntityState.Modified;
            return await _context.SaveChangesAsync();
        }

        public void UpdateEntity(T entity)
        {
            _context.Set<T>().Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }
    }
}
