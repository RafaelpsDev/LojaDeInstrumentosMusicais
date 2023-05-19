using LojaDeInstrumentosMusicais.Application.DTOs;
using LojaDeInstrumentosMusicais.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LojaDeInstrumentosMusicais.Application.Interfaces
{
    public interface IVendedorAdapter
    {
        public VendedorModel ToVendedorModel(VendedorRequestDTO vendedorRequestDTO);
        public VendedorResponseDTO ToVendedorResponseDTO(VendedorModel vendedorModel);
    }
}
