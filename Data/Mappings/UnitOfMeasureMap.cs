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
            builder.ToTable("UnitOfMeasure");

            // Chave primaria
            builder.HasKey(un => un.Id);

            //Identity
            builder.Property(un => un.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn();

            // Propriedades
            builder.Property(un => un.Name)
                .IsRequired()
                .HasColumnName("Name")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(80);

            builder.Property(un => un.Abbreviation)
                .IsRequired()
                .HasColumnName("Abbreviation")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(8);



            ////Indices
            builder.HasIndex(u => u.Abbreviation, "IX_UnitOfMeasure_Abbreviation")
                   .IsUnique();



            //Relacionamentos

            //Relacionamento tendo muito produtos no mapping de products
        }
    }
}
