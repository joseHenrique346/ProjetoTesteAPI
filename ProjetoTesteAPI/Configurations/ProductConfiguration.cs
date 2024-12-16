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
                .WithMany()
                .HasForeignKey(p => p.BrandId)
                .IsRequired();

            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.CodeNumber).IsRequired();
            builder.Property(x => x.Description).IsRequired();
        }
    }
}
