using NinjectPractice.Models;
using System.Data.Entity;

namespace NinjectPractice.Repositories
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(string nameOrConnectionString) : base(nameOrConnectionString) {}

        public DbSet<Product> Products { get; set; }
    }
}