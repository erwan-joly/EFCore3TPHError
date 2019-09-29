using Microsoft.EntityFrameworkCore;

namespace POC_EF_Core_3
{
    public class PocDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql("Host=localhost;Database=test;Username=postgres;Password=postgres");

        public virtual DbSet<ItemInstance> ItemInstance { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}