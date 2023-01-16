using FileStorage.DAL.EF;
using FileStorage.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace FileStorage.DAL.Repositories
{
    public class GenericRepository<TId, TEntity> : IRepository<TId, TEntity> where TEntity : class
    {
        private readonly DbSet<TEntity> _entities;

        public GenericRepository(FileStorageContext context)
        {
            _entities = context.Set<TEntity>();
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            var addedEntity = await _entities.AddAsync(entity);
            return addedEntity.Entity;
        }

        public async Task DeleteAsync(TId id)
        {
            var entity = await _entities.FindAsync(id);
            await DeleteAsync(entity);
        }

        public Task DeleteAsync(TEntity entity)
        {
            _entities.Remove(entity);
            return Task.CompletedTask;
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _entities.ToListAsync();
        }

        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicator , IEnumerable<string> includeProperties = null)
        {
            var query = _entities.AsQueryable();
            if (includeProperties != null && includeProperties.Count() != 0)
            {
                foreach (var property in includeProperties)
                {
                    query.Include(property);
                }
            }
            return await query.FirstOrDefaultAsync(predicator);
        }

        public async Task<TEntity> GetByIdAsync(TId id, IEnumerable<string> includeProperties = null)
        {
            var query = _entities.AsQueryable();
            if (includeProperties != null && includeProperties.Count() != 0)
            {
                foreach (var property in includeProperties)
                {
                    query.Include(property);
                }
            }
            return await Task.FromResult(((DbSet<TEntity>)query).Find(id));
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            _entities.Attach(entity).State = EntityState.Modified;
            return await Task.FromResult(entity);
        }
    }
}
