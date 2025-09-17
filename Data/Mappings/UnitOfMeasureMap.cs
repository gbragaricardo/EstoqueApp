using EstoqueApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EstoqueApp.Data.Mappings
{
    public class UnitOfMeasureMap : IEntityTypeConfiguration<UnitOfMeasure>
    {
        public void Configure(EntityTypeBuilder<UnitOfMeasure> builder)
        {
            // Tabela do banco
            builder.ToTable("UnitOfMeasure", t =>
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
                .HasMaxLength(80);


            //Indices
            builder.HasIndex(x => x.Sku, "IX_Product_Sku")
                .IsUnique();

            //Relacionamentos
            builder.HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId)
                .HasConstraintName("FK_Product_Category")
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
