namespace PocketBook.Core.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using PocketBook.Core.IRepositories;
    using PocketBook.Data;
    using PocketBook.Model;

    public class Respository<T> : IRespository<T> where T : class
    {
        protected ApplicationDbContext _applicationDbContext;
        protected DbSet<T> _dbSet;
        protected readonly ILogger _logger;

        public Respository(ApplicationDbContext applicationDbContext, ILogger logger)
        {
            _applicationDbContext = applicationDbContext;
            _dbSet =  applicationDbContext.Set<T>();
            _logger = logger;
        }

        public virtual async Task<bool> AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            return true;
        }

        public virtual Task<bool> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public virtual async Task<T> GetByIdAsync(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

        public virtual Task<bool> UpsertAsync(T entity)
        {
            throw new NotImplementedException();
        }
    }
}