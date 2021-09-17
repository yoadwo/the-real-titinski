using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Titinski.WebAPI.Interfaces.Repositories
{
    public interface IGenericRepository<T> where T : class, Models.IEntity
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetAsync(string id);
        // not sure how to make async
        // find() must receive object[]: params, not  expressionFunc:expression
        //Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> expression);
        T Add(T entity);
        Task<T> UpdateAsync(T entity);
        Task<T> DeleteAsync(T entity);

        //void AddRange(IEnumerable<T> entities);        
        //void RemoveRange(IEnumerable<T> entities);
    }
}
