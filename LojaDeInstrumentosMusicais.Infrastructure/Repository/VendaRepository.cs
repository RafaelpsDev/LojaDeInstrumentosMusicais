using LojaDeInstrumentosMusicais.Application.Interfaces;
using LojaDeInstrumentosMusicais.Domain.Models;
using LojaDeInstrumentosMusicais.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LojaDeInstrumentosMusicais.Infrastructure.Repository
{
    public class VendaRepository : IVendaRepository
    {
        private readonly LojaDeInstrumentosMusicaisContext _context;
        public VendaRepository(LojaDeInstrumentosMusicaisContext context)
        {
            _context = context;
        }
        public async Task<VendaModel> AtualizarVenda(int id, VendaModel vendaModel)
        {
            var vendaAtualizar = await BuscarVendaPorId(id);
            
            vendaAtualizar.StatusDaVenda = vendaModel.StatusDaVenda;
            vendaAtualizar.DataDeAlteracao = vendaModel.DataDeAlteracao;
            vendaAtualizar.IdVendedor = vendaAtualizar.IdVendedor;
            vendaAtualizar.DataDaVenda = vendaAtualizar.DataDaVenda;

            _context.Vendas.Update(vendaAtualizar);
            await _context.SaveChangesAsync();
            return vendaAtualizar;
        }

        public Task<VendaModel> BuscarVendaPorId(int id)
        {
            return _context.Vendas.Include(v => v.Vendedor).Include(v => v.Instrumentos).FirstOrDefaultAsync(v => v.Id == id);
        }

        public async Task<VendaModel> RegistrarVenda(VendaModel vendaModel)
        {
            await _context.Vendas.AddAsync(vendaModel);
            await _context.SaveChangesAsync();
            return vendaModel;
        }
    }
}
