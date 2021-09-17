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
        protected readonly ImagesDbContext _context;
        public GenericRepository(ImagesDbContext context)
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
        
        public T Add(T entity)
        {
            _context.Set<T>().Add(entity);
            return entity;
        }

        public T Update(T entity)
        {
            var entityEntry = _context.Set<T>().Update(entity);
            return entityEntry.Entity;
        }

        public T Delete(T entity)
        {
            var entityEntry = _context.Set<T>().Remove(entity);
            return entityEntry.Entity;
        }
    }
}
