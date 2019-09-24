using Microsoft.EntityFrameworkCore;

namespace POC_EF_Core_3
{
    public static class ModelBuilderExtensions
    {
        public static void RemovePluralizingTableNameConvention(this ModelBuilder modelBuilder)
        {
            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                entity.SetTableName(entity.DisplayName());
            }
        }
    }

    public class PocDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql("Host=localhost;Database=test;Username=postgres;Password=postgres");

        public virtual DbSet<EquipmentOption> EquipmentOption { get; set; }

        public virtual DbSet<ItemInstance> ItemInstance { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.RemovePluralizingTableNameConvention();//without this line it works

            // build TPH tables for inheritance
            modelBuilder.Entity<ItemInstance>()
                .HasDiscriminator<string>("Discriminator")
                .HasValue<WearableInstance>("WearableInstance");
        }
    }
}