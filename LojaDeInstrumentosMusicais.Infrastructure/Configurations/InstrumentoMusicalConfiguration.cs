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
    public class InstrumentoMusicalConfiguration : IEntityTypeConfiguration<InstrumentoMusicalModel>
    {
        public void Configure(EntityTypeBuilder<InstrumentoMusicalModel> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.IdVenda).IsRequired();
            builder.Property(x => x.Nome).IsRequired().HasColumnType("VARCHAR(14)");
            builder.Property(x => x.Valor).IsRequired();
        }
    }
}
