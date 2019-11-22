using Barbecue.ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;

namespace Barbecue.Infrastructure.Data
{
    public class EfBaseContext : DbContext
    {
        public EfBaseContext(DbContextOptions<EfBaseContext> options) : base(options) { }

        public DbSet<Event> Events { get; set; }
        public DbSet<User> Users { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
        }
    }
}