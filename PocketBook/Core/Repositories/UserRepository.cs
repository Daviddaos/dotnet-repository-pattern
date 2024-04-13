using Microsoft.EntityFrameworkCore;
using PocketBook.Core.IRepositories;
using PocketBook.Data;
using PocketBook.Model;

namespace PocketBook.Core.Repositories
{
    class UserRepository : Respository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext applicationDbContext, ILogger logger) : base (applicationDbContext, logger)
        {
            
        }
        
        public override async Task<bool> AddAsync(User entity)
        {
            await _dbSet.AddAsync(entity);
            return true;
        }

        public override async Task<bool> DeleteAsync(Guid id)
        {
            try
            {
                var existingEntity = await _dbSet.Where(u => u.Id == id).FirstOrDefaultAsync();

                if (existingEntity != null)
                {
                    _dbSet.Remove(existingEntity);
                    return true;
                }
                
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} GetAll method error", typeof(UserRepository));
                return false;
            }
        }

        public override async Task<IEnumerable<User>> GetAllAsync()
        {
            try
            {
                return await _dbSet.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} GetAll method error", typeof(UserRepository));
                return new List<User>();
            }
        }

        public override async Task<User> GetByIdAsync(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

        public override async Task<bool> UpsertAsync(User entity)
        {
            try
            {                
                var existingEntity = await _dbSet.Where(u => u.Id == entity.Id).FirstOrDefaultAsync();

                if (existingEntity == null)
                {
                    return await AddAsync(entity);
                }

                existingEntity.Email = entity.Email;
                existingEntity.FirstName = entity.FirstName;
                existingEntity.LastName = entity.LastName;
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} GetAll method error", typeof(UserRepository));
                return false;
            }
        }
    }
}