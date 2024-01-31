using Microsoft.EntityFrameworkCore;
using System.Reflection;
using User.Domain;
using User.Domain.Entities;

namespace User.Infrastructure
{
    public class UserDbContext : DbContext
    {
        public DbSet<UserModel> Users { get; set; }
        public DbSet<UserLoginHistory> UserLoginHistories { get; set; }

        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
