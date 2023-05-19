using LojaDeInstrumentosMusicais.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LojaDeInstrumentosMusicais.Application.Interfaces
{
    public interface IVendaRepository
    {
        Task<VendaModel> BuscarVendaPorId(int id);
        Task<VendaModel> RegistrarVenda(VendaModel vendaModel);
        Task<VendaModel> AtualizarVenda(int id, VendaModel vendaModel);
    }
}
