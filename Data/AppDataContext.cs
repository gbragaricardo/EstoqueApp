using EstoqueApp.Data.Mappings;
using EstoqueApp.Models;
using Microsoft.EntityFrameworkCore;

namespace EstoqueApp.Data
{
    public class AppDataContext :DbContext
    {

        public DbSet<Product> Products { get; set; }
        public DbSet<StockMovement> StockMovements { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CostCenter> CostCenters { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost,1433;Database=Estoque;User ID=sa;Password=Projeta24862");
            //optionsBuilder.LogTo(Console.WriteLine);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProductMap());
            modelBuilder.ApplyConfiguration(new StockMovementMap());
            modelBuilder.ApplyConfiguration(new CategoryMap());
            modelBuilder.ApplyConfiguration(new CostCenterMap());
        }
    }
}