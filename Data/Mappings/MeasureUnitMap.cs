using EstoqueApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EstoqueApp.Data.Mappings
{
    public class MeasureUnitMap : IEntityTypeConfiguration<MeasureUnit>
    {
        public void Configure(EntityTypeBuilder<MeasureUnit> builder)
        {
            // Tabela do banco
            builder.ToTable("MeasureUnit");

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
            builder.HasIndex(u => u.Abbreviation, "IX_MeasureUnit_Abbreviation")
                   .IsUnique();



            //Relacionamentos

            //Relacionamento tendo muito produtos no mapping de products
        }
    }
}
