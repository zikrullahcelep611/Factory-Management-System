using FabrikaYonetimSistemi.Entity.Entities;
using Microsoft.EntityFrameworkCore;

namespace FabrikaYonetimSistemi.Data.DataContext
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }

        public DbSet<Factory> Factories { get; set; }
        public DbSet<Building> Buildings { get; set; }
        public DbSet<Storage> Storages { get; set; }
        public DbSet<Personnel> Personnels { get; set; }
        public DbSet<Material> Materials { get; set; }
        public DbSet<MaterialTransaction> MaterialTransactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Factory>()
                .HasMany(f => f.Buildings).WithOne(b => b.Factory)
                .HasForeignKey(b => b.FactoryId);

            modelBuilder.Entity<Building>()
                .HasMany(b => b.Storages).WithOne(s => s.Building)
                .HasForeignKey(s => s.BuildingId);

           /* modelBuilder.Entity<MaterialTransaction>()
                .HasOne(mt => mt.Material).WithMany(m => m.MaterialTransactions)
                .HasForeignKey(m => m.MaterialId);

            modelBuilder.Entity<MaterialTransaction>()
                .HasOne(mt => mt.Personnel)
                .WithMany(p => p.MaterialTransactions)
                .HasForeignKey(mt => mt.PersonelId);*/
        }
    }
}
