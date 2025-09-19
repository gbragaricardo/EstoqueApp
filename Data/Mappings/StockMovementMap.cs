using EstoqueApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EstoqueApp.Data.Mappings
{
    public class StockMovementMap : IEntityTypeConfiguration<StockMovement>
    {
        public void Configure(EntityTypeBuilder<StockMovement> builder)
        {
            // Tabela do banco
            builder.ToTable("StockMovement", t =>
            {
                t.HasCheckConstraint("CK_StockMovement_Quantity_Positive", "[Quantity] > 0");
                t.HasCheckConstraint("CK_StockMovement_Type_Allowed", "[Type] IN ('Entry','Exit','Transfer')");
            });

            // Chave primaria
            builder.HasKey(m => m.Id);

            // Identity
            builder.Property(m => m.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn();

            // Propriedades

            builder.Property(m => m.Type)
                .IsRequired()
                .HasColumnName("Type")
                .HasConversion<string>() // Para converter e salvar como texto
                .HasMaxLength(16)
                .IsUnicode(false);

            builder.Property(m => m.Quantity)
                .IsRequired()
                .HasColumnName("Quantity")
                .HasColumnType("decimal(18,2)");

            builder.Property(m => m.Date)
                .IsRequired()
                .HasColumnName("Date")
                .HasColumnType("datetime2")
                .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(m => m.Description)
               .HasColumnName("Description")
               .HasColumnType("NVARCHAR")
               .HasMaxLength(256);

            //Indices

            // Índices úteis para consultas e relatórios
            builder.HasIndex(m => m.ProductId, "IX_StockMovement_Product");

            builder.HasIndex(m => m.OriginCostCenterId, "IX_StockMovement_OriginCC");

            builder.HasIndex(m => m.DestinationCostCenterId, "IX_StockMovement_DestinationCC");

            builder.HasIndex(m => m.Date, "IX_StockMovement_Date");


            // Relacionamentos

            // Relacionamento 1:N com movimentacao tendo um produto, e produto tendo N movimentacoes
            builder.HasOne(m => m.Product)
                .WithMany(p => p.Movements)
                .HasForeignKey(m => m.ProductId)
                .HasConstraintName("FK_Movement_Product")
                .OnDelete(DeleteBehavior.Restrict);

            // Relacionamento 1:N com movimentacao tendo um centro de custo, e centro de custo tendo N movimentações
            builder.HasOne(m => m.OriginCostCenter)
               .WithMany()
               .HasForeignKey(m => m.OriginCostCenterId)
               .HasConstraintName("FK_Movement_OriginCostCenter")
               .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(m => m.DestinationCostCenter)
                .WithMany()
                .HasForeignKey(m => m.DestinationCostCenterId)
                .HasConstraintName("FK_Movement_DestinationCostCenter")
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
