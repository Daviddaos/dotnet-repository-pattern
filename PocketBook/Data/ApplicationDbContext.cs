namespace PocketBook.Data
{
    using Microsoft.EntityFrameworkCore;
    using PocketBook.Model;

    public class ApplicationDbContext : DbContext
    {
        public virtual DbSet<User> Users {get; set;}
        
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base (options)
        {
        }
        
    }
}