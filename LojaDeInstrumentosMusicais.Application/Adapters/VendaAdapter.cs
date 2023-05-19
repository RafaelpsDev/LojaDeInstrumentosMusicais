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
    public class VendaAdapter : IVendaAdapter
    {
        private readonly IVendedorAdapter _vendedorAdapter;
        private readonly IInstrumentoMusicalAdapter _instrumentosMusicaisAdapter;

        public VendaAdapter(IVendedorAdapter vendedorAdapter, IInstrumentoMusicalAdapter instrumentosMusicaisAdapter)
        {
            _vendedorAdapter = vendedorAdapter;
            _instrumentosMusicaisAdapter = instrumentosMusicaisAdapter;
        }
        public VendaModel ToVendaModel(VendaRequestDTO vendaRequestDTO)
        {
            return new VendaModel
            {
                Vendedor = _vendedorAdapter.ToVendedorModel(vendaRequestDTO.Vendedor),
                Instrumentos = _instrumentosMusicaisAdapter.ToInstrumentoMusicalModel(vendaRequestDTO.Instrumentos)
            };
        }

        public VendaModel ToVendaModelUpdate(VendaRequestUpdateDTO vendaRequestUpdateDTO)
        {
            return new VendaModel
            {
                DataDeAlteracao = vendaRequestUpdateDTO.DataDeAlteracao,
                StatusDaVenda = vendaRequestUpdateDTO.StatusDaVenda
            };
        }

        public VendaResponseDTO ToVendaResponse(VendaModel vendaModel)
        {
            return new VendaResponseDTO
            {
                Id = vendaModel.Id,
                IdPedido = vendaModel.IdPedido,
                IdVendedor = vendaModel.IdVendedor,
                DataDaVenda = vendaModel.DataDaVenda,
                StatusDaVenda = vendaModel.StatusDaVenda,
                Instrumentos = _instrumentosMusicaisAdapter.ToInstrumentoMusicalResponse(vendaModel.Instrumentos),
                Vendedor = _vendedorAdapter.ToVendedorResponseDTO(vendaModel.Vendedor)
            };
        }
    }
}
