using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace FileStorage.DAL.Interfaces
{
    public interface IRepository<TId , TEntity> where TEntity : class
    {
        Task<TEntity> AddAsync(TEntity entity);

        Task<TEntity> GetByIdAsync(TId id , IEnumerable<string> includeProperties = null);

        Task<TEntity> GetAsync(Expression<Func<TEntity , bool>> predicator , IEnumerable<string> includeProperties = null);

        Task<TEntity> UpdateAsync(TEntity entity);

        Task DeleteAsync(TId id);

        Task DeleteAsync(TEntity entity);

        Task<IEnumerable<TEntity>> GetAllAsync();
    }
}
