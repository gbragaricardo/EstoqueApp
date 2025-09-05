using EstoqueApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EstoqueApp.Data.Mappings
{
    public class CostCenterMap : IEntityTypeConfiguration<CostCenter>
    {
        public void Configure(EntityTypeBuilder<CostCenter> builder)
        {
            // Tabela do banco
            builder.ToTable("CostCenter");

            // Chave primaria
            builder.HasKey(c => c.Id);

            //Identity
            builder.Property(c => c.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn();

            // Propriedades
            builder.Property(c => c.Name)
                .IsRequired()
                .HasColumnName("Name")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(80);

            builder.Property(c => c.Code)
                .IsRequired()
                .HasColumnName("Code")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(8);

            //Índices

            builder.HasIndex(c => c.Code)
                .IsUnique();

            //Relacionamentos

            //Relacionamento 1:N com movements feito no StockMovementMap
        }
    }
}
