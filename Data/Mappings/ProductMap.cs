using EstoqueApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EstoqueApp.Data.Mappings
{
    public class ProductMap : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            // Tabela do banco
            builder.ToTable("Product", t =>
            {
                t.HasCheckConstraint("CK_Product_CurrentStock_NonNegative", "[CurrentStock] >= 0");
                t.HasCheckConstraint("CK_Product_UnitPrice_NonNegative", "[UnitPrice]   >= 0");
            });

            // Chave primaria
            builder.HasKey(p => p.Id);

            //Identity
            builder.Property(p => p.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn();

            // Propriedades
            builder.Property(p => p.Name)
                .IsRequired()
                .HasColumnName("Name")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(160);

            builder.Property(p => p.UnitPrice)
                .IsRequired()
                .HasColumnName("UnitPrice")
                .HasColumnType("decimal(18,2)")
                .HasDefaultValue(0m);

            builder.Property(p => p.Description)
                .HasColumnName("Description")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(256);

            builder.Property(p => p.Sku)
                .IsRequired()
                .HasColumnName("SKU")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(8);

            builder.Property(p => p.MinStock)
               .IsRequired()
               .HasColumnName("MinStock")
               .HasColumnType("decimal(18,2)")
               .HasDefaultValue(0);

            builder.Property(p => p.CurrentStock)
                .IsRequired()
                .HasColumnName("CurrentStock")
                .HasColumnType("decimal(18,2)")
                .HasDefaultValue(0);

            builder.Property(p => p.IsActive)
                .IsRequired()
                .HasColumnName("IsActive")
                .HasColumnType("bit")
                .HasDefaultValue(true);


            //Indices
            builder.HasIndex(p => p.Name, "IX_ProductName")
                .IsUnique();

            builder.HasIndex(x => x.Sku, "IX_Product_Sku")
                .IsUnique();

            //Relacionamentos
            builder.HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId)
                .HasConstraintName("FK_Product_Category")
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.UnitOfMeasure)
                .WithMany(un => un.Products)
                .HasForeignKey(p => p.UnitOfMeasureId)
                .HasConstraintName("FK_Product_UnitOfMeasure")
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
