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
    public class VendaConfiguration : IEntityTypeConfiguration<VendaModel>
    {
        public void Configure(EntityTypeBuilder<VendaModel> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.IdPedido).IsRequired();
            builder.Property(x => x.IdVendedor).IsRequired();
            builder.Property(x => x.StatusDaVenda).IsRequired();
            builder.Property(x => x.DataDaVenda).IsRequired();
            builder.Property(x => x.DataDeAlteracao);
        }
    }
}
