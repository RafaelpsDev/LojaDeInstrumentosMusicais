using LojaDeInstrumentosMusicais.Application.DTOs;
using LojaDeInstrumentosMusicais.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LojaDeInstrumentosMusicais.Application.Interfaces
{
    public interface IVendaService
    {
        Task<VendaResponseDTO> BuscarVendaPorId(int id);
        Task<VendaResponseDTO> RegistrarVenda(VendaRequestDTO vendaRequestDTO);
        Task<VendaResponseDTO> AtualizarVenda(int id, VendaRequestUpdateDTO vendaRequestUpdateDTO);
    }
}
