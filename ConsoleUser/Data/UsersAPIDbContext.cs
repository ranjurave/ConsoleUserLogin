using ConsoleUser.Models;
using Microsoft.EntityFrameworkCore;

namespace ConsoleUser.Data
{
    public class UsersAPIDbContext : DbContext
    {
        public UsersAPIDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
    }
}
