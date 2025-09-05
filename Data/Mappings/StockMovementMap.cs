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
            builder.ToTable("StockMovements", t =>
            {
                t.HasCheckConstraint("CK_StockMovement_Quantity_Positive", "[Quantity] > 0");
                t.HasCheckConstraint("CK_StockMovement_UnitCost_NonNegative", "[UnitCost] >= 0");
                t.HasCheckConstraint("CK_StockMovement_Type_Allowed", "[Type] IN ('In','Out')");
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
                .HasColumnType("VARCHAR")
                .HasMaxLength(10)
                .IsUnicode(false);

            builder.Property(m => m.UnitCost)
                .IsRequired()
                .HasColumnName("UnitCost")
                .HasColumnType("decimal(18,2)")
                .HasDefaultValue(0m);

            builder.Property(m => m.Quantity)
                .IsRequired()
                .HasColumnName("Quantity")
                .HasColumnType("int");

            builder.Property(m => m.CreateDate)
                .IsRequired()
                .HasColumnName("CreateDate")
                .HasColumnType("datetime2")
                .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(m => m.Notes)
               .HasColumnName("Description")
               .HasColumnType("NVARCHAR")
               .HasMaxLength(256);

            //Indices


            // Relacionamentos

            // Relacionamento 1:N com movimentacao tendo um produto, e produto tendo N movimentacoes
            builder.HasOne(m => m.Product)
                .WithMany(p => p.Movements)
                .HasForeignKey(m => m.ProductId)
                .HasConstraintName("FK_Movement_Product")
                .OnDelete(DeleteBehavior.Restrict);

            // Relacionamento 1:N com movimentacao tendo um centro de custo, e centro de custo tendo N movimentações
            builder.HasOne(m => m.CostCenter)
                .WithMany(c => c.Movements)
                .HasForeignKey(m => m.CostCenterId)
                .HasConstraintName("FK_Movement_CostCenter")
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
