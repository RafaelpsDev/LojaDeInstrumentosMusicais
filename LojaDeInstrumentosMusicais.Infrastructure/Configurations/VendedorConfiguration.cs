using LojaDeInstrumentosMusicais.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LojaDeInstrumentosMusicais.Infrastructure.Configurations
{
    public class VendedorConfiguration : IEntityTypeConfiguration<VendedorModel>
    {
        public void Configure(EntityTypeBuilder<VendedorModel> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Cpf).IsRequired().HasColumnType("VARCHAR(14)");
            builder.Property(x => x.Nome).IsRequired().HasColumnType("VARCHAR(150)");
            builder.Property(x => x.Email).IsRequired().HasColumnType("VARCHAR(150)");
            builder.Property(x => x.Telefone).HasColumnType("VARCHAR(20)");
        }
    }
}
