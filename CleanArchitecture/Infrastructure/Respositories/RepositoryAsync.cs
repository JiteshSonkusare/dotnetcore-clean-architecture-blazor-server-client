using Application.Interfaces.Repositories;
using Domain.Contracts;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Respositories
{
    public class RepositoryAsync<T, TId> : IRepositoryAsync<T, TId?> where T : AuditableEntity<TId>
    {
        private readonly CleanArchitectureDBContext _dbContext;

        public RepositoryAsync(CleanArchitectureDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IQueryable<T>> Entities()
        {
            var data = await _dbContext.Set<T>().ToListAsync();
            return data.AsQueryable();
        }

        public async Task<T> AddAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            return entity;
        }

        public Task DeleteAsync(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            return Task.CompletedTask;
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIdAsync(TId? id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public Task UpdateAsync(T entity)
        {
            T? exist = _dbContext.Set<T>().Find(entity.Id);
            _dbContext?.Entry(exist).CurrentValues.SetValues(entity);
            return Task.CompletedTask;
        }
    }
}
