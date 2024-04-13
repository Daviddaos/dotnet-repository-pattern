using Microsoft.EntityFrameworkCore;
using PocketBook.Core.IConfiguratoin;
using PocketBook.Core.IRepositories;
using PocketBook.Core.Repositories;
using PocketBook.Model;

namespace PocketBook.Data
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly ILogger _logger;
        public IUserRepository Users {get; private set; }

        public UnitOfWork(ApplicationDbContext applicationDbContext, ILoggerFactory loggerFactory)
        {
            _applicationDbContext = applicationDbContext;
            _logger = loggerFactory.CreateLogger("logs");
            Users = new UserRepository(_applicationDbContext, _logger);
        }

        public async Task SaveChangesAsync()
        {
            await _applicationDbContext.SaveChangesAsync();
        }        

        public void Dispose()
        {
            _applicationDbContext.Dispose();
        }
    }
}