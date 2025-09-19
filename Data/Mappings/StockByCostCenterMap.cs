using EstoqueApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EstoqueApp.Data.Mappings
{
    public class StockByCostCenterMap : IEntityTypeConfiguration<StockByCostCenter>
    {
        public void Configure(EntityTypeBuilder<StockByCostCenter> builder)
        {
            builder.ToTable("StockByCostCenter");

            builder.HasKey(s => s.Id);

            // Identity
            builder.Property(m => m.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn();

            builder.Property(s => s.Quantity)
                .IsRequired()
                .HasColumnName("Quantity")
                .HasColumnType("decimal(18,2)");

            builder.Property(s => s.LastUpdated)
                .IsRequired()
                .HasColumnName("LastUpdate")
                .HasColumnType("datetime2")
                .HasDefaultValueSql("GETUTCDATE()");

            // Relacionamento com Product
            builder.HasOne(x => x.Product)
                   .WithMany(p => p.StocksByCostCenter)
                   .HasForeignKey(x => x.ProductId)
                   .HasConstraintName("FK_StockByCC_Product")
                   .OnDelete(DeleteBehavior.SetNull);

            // Relacionamento com CostCenter
            builder.HasOne(x => x.CostCenter)
                   .WithMany(c => c.StocksByCostCenter)
                   .HasForeignKey(x => x.CostCenterId)
                   .HasConstraintName("FK_StockByCC_CostCenter")
                   .OnDelete(DeleteBehavior.SetNull);

            //Índices
            builder.HasIndex(s => new { s.ProductId, s.CostCenterId })
            .IsUnique();

        }
    }
}
