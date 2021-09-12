using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Titinski.WebAPI.Interfaces.Repositories;

namespace Titinski.WebAPI.EFCore.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class, Models.IEntity
    {
        protected readonly ImageRepoDbContext _context;
        public GenericRepository(ImageRepoDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> GetAsync(string id)
        {
            return await _context.Set<T>().FindAsync(id);
        }
        
        public async Task<T> AddAsync(T entity)
        {
            _context.Set<T>().Add(entity);
            // should move to UnitOfWork
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            var entityEntry = _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
            return entityEntry.Entity;
        }

        public async Task<T> DeleteAsync(T entity)
        {
            var entityEntry = _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
            return entityEntry.Entity;
        }
    }
}
