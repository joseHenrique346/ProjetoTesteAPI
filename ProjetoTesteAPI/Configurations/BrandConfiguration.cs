using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjetoTesteAPI.Models;

namespace ProjetoTesteAPI.Configurations
{
    public class BrandConfiguration : IEntityTypeConfiguration<Brand>
    {
        public void Configure(EntityTypeBuilder<Brand> builder)
        {
            builder.ToTable("marca");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(40)
                .HasColumnName("nome");

            builder.Property(x => x.CodeNumber)
                .IsRequired()
                .HasMaxLength(6)
                .HasColumnName("codigo");

            builder.Property(x => x.Description)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("descricao");
        }
    }
}
