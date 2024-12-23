﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjetoTesteAPI.Models;

namespace ProjetoTesteAPI.Configurations
{
    public class ClientConfiguration : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.ToTable("cliente");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).HasColumnName("nome");
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.Name).HasMaxLength(40);

            builder.Property(x => x.Email).HasColumnName("email");
            builder.Property(x => x.Email).IsRequired();
            builder.Property(x => x.Email).HasMaxLength(60);

            builder.Property(x => x.CPF).HasColumnName("cpf");
            builder.Property(x => x.CPF).IsRequired();
            builder.Property(x => x.CPF).HasMaxLength(11);
            builder.HasIndex(x => x.CPF).IsUnique();

            builder.Property(x => x.Phone).HasColumnName("telefone");
            builder.Property(x => x.Phone).IsRequired();
            builder.Property(x => x.Phone).HasMaxLength(11);
        }
    }
}