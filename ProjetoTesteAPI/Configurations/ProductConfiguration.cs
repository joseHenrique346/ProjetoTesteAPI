﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjetoTesteAPI.Models;

namespace ProjetoTesteAPI.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("produto");

            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Brand)
                .WithMany(z => z.ListProduct)
                .HasForeignKey(p => p.BrandId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);

            builder.Property(x => x.Name).HasColumnName("nome");
            builder.Property(x => x.Name).HasMaxLength(40);
            builder.Property(x => x.Name).IsRequired();

            builder.Property(x => x.Code).HasColumnName("codigo");
            builder.Property(x => x.Code).HasMaxLength(6);
            builder.Property(x => x.Code).IsRequired();

            builder.Property(x => x.Description).HasColumnName("descricao");
            builder.Property(x => x.Description).HasMaxLength(100);
            builder.Property(x => x.Description).IsRequired();

            builder.Property(x => x.Stock).HasColumnName("estoque");
            builder.Property(x => x.Stock).HasMaxLength(6);
            builder.Property(x => x.Stock).IsRequired();
        }
    }
}