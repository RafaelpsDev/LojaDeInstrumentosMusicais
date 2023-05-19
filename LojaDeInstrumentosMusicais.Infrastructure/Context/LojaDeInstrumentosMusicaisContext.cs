using LojaDeInstrumentosMusicais.Domain.Models;
using LojaDeInstrumentosMusicais.Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LojaDeInstrumentosMusicais.Infrastructure.Context
{
    public class LojaDeInstrumentosMusicaisContext : DbContext
    {
        public LojaDeInstrumentosMusicaisContext(DbContextOptions<LojaDeInstrumentosMusicaisContext> options) : base(options)
        {
            
        }
        public DbSet<VendedorModel> Vendedores { get; set; }
        public DbSet<InstrumentoMusicalModel> Instrumentos { get; set; }
        public DbSet<VendaModel> Vendas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<VendaModel>().
                HasOne(venda => venda.Vendedor).
                WithMany(vendedores => vendedores.Vendas).
                HasForeignKey(vendas => vendas.IdVendedor);

            modelBuilder.Entity<InstrumentoMusicalModel>().
                HasOne(i => i.Venda).
                WithMany(v => v.Instrumentos).
                HasForeignKey(i => i.IdVenda);


            modelBuilder.ApplyConfiguration(new VendedorConfiguration());
            modelBuilder.ApplyConfiguration(new InstrumentoMusicalConfiguration());
            modelBuilder.ApplyConfiguration(new VendaConfiguration());

            base.OnModelCreating(modelBuilder);
        }

    }
}
