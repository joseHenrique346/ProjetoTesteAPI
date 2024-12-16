using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjetoTesteAPI.Models;

namespace ProjetoTesteAPI.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Brand)
                .WithMany(z => z.ListProduct)
                .HasForeignKey(p => p.BrandId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);

            builder.Property(x => x.Name)
                .HasMaxLength(40)
                .HasColumnName("nome")
                .IsRequired();

            builder.Property(x => x.CodeNumber)
                .HasMaxLength(6)
                .HasColumnName("codigo")
                .IsRequired();

            builder.Property(x => x.Description)
                .HasMaxLength(100)
                .HasColumnName("descricao")
                .IsRequired();
        }
    }
}
