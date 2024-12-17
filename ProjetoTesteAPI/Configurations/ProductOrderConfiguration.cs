using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjetoTesteAPI.Models;

namespace ProjetoTesteAPI.Configurations
{
    public class ProductOrderConfiguration : IEntityTypeConfiguration<ProductOrder>
    {
        public void Configure(EntityTypeBuilder<ProductOrder> builder)
        {
            builder.HasKey(x => new {x.OrderId, x.ProductId});

            builder.HasOne(x => x.Order)
                .WithMany(y => y.ProductOrders)
                .HasForeignKey(x => x.OrderId);

            builder.HasOne(x => x.Product)
                   .WithMany()
                   .HasForeignKey(x => x.ProductId);
                   
        }
    }
}
