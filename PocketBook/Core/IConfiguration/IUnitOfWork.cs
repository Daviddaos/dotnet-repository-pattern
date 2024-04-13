namespace PocketBook.Core.IConfiguratoin
{
    using PocketBook.Core.IRepositories;

    public interface IUnitOfWork
    {
        IUserRepository Users { get; }
        Task SaveChangesAsync();
    }
}