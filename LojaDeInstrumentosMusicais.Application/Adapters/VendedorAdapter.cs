using LojaDeInstrumentosMusicais.Application.DTOs;
using LojaDeInstrumentosMusicais.Application.Interfaces;
using LojaDeInstrumentosMusicais.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LojaDeInstrumentosMusicais.Application.Adapters
{
    public class VendedorAdapter : IVendedorAdapter
    {
        public VendedorModel ToVendedorModel(VendedorRequestDTO vendedorRequestDTO)
        {
            return new VendedorModel
            {
                Nome = vendedorRequestDTO.Nome,
                Cpf = vendedorRequestDTO.Cpf,
                Email = vendedorRequestDTO.Email,
                Telefone = vendedorRequestDTO.Telefone
            };
        }
        public VendedorResponseDTO ToVendedorResponseDTO(VendedorModel vendedorModel)
        {
            return new VendedorResponseDTO
            {
                Id = vendedorModel.Id,
                Nome = vendedorModel.Nome,
                Cpf = vendedorModel.Cpf,
                Email = vendedorModel.Email,
                Telefone = vendedorModel.Telefone
            };
        }
    }
}
