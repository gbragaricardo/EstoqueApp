using Microsoft.EntityFrameworkCore;

namespace EstoqueApp.Data
{
    public class AppDataContext :DbContext
    {

        //public DbSet<Category> Categories{ get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost,1433;Database=Estoque;User ID=sa;Password=Projeta24862");
            //optionsBuilder.LogTo(Console.WriteLine);
        }
    }
}