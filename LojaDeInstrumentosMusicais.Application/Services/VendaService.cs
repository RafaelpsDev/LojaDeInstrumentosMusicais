using LojaDeInstrumentosMusicais.Application.DTOs;
using LojaDeInstrumentosMusicais.Application.Interfaces;
using LojaDeInstrumentosMusicais.Domain.Enums;
using LojaDeInstrumentosMusicais.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LojaDeInstrumentosMusicais.Application.Services
{
    public class VendaService : IVendaService
    {       
        private readonly IVendaRepository _repository;
        private readonly IVendaAdapter _adapter;
        public VendaService(IVendaRepository repository, IVendaAdapter adapter)
        {
            _repository = repository;
            _adapter = adapter;
        }
        public async Task<VendaResponseDTO> BuscarVendaPorId(int id)
        {
            var venda = await _repository.BuscarVendaPorId(id);
            var toVendaResponse = _adapter.ToVendaResponse(venda);
            return toVendaResponse ?? throw new Exception("Por favor, informar um Id de venda válido.");
        }

        public async Task<VendaResponseDTO> RegistrarVenda(VendaRequestDTO vendaRequestDTO)
        {
            var toVendaModel = _adapter.ToVendaModel(vendaRequestDTO);
            var vendaRegistrada = await _repository.RegistrarVenda(toVendaModel);
            var toResponseVenda = _adapter.ToVendaResponse(vendaRegistrada);
            return toResponseVenda;
        }
        public async Task<VendaResponseDTO> AtualizarVenda(int id, VendaRequestUpdateDTO vendaRequestUpdateDTO)
        {
            var vendaAtualizar = await BuscarVendaPorId(id) ?? throw new Exception("Por favor, informar um Id de venda válido.");

            if (vendaAtualizar.StatusDaVenda == StatusDaVenda.AguardandoPagamento)
            {
                if (vendaRequestUpdateDTO.StatusDaVenda != StatusDaVenda.PagamentoAprovado && vendaRequestUpdateDTO.StatusDaVenda != StatusDaVenda.Cancelado)
                {
                    throw new Exception("A venda esta com o status de Aguardando Pagamento, por favor escolha 'Pagamento Aprovado' ou 'Cancelado'!");
                }
            }
            if (vendaAtualizar.StatusDaVenda == StatusDaVenda.PagamentoAprovado)
            {
                if (vendaRequestUpdateDTO.StatusDaVenda != StatusDaVenda.EnviadoParaTransportadora && vendaRequestUpdateDTO.StatusDaVenda != StatusDaVenda.Cancelado)
                {
                    throw new Exception("A venda esta com o status de Pagamento Aprovado, por favor escolha 'Enviar para Transportadora' ou 'Cancelada'!");
                }
            }
            if (vendaAtualizar.StatusDaVenda == StatusDaVenda.EnviadoParaTransportadora)
            {
                if (vendaRequestUpdateDTO.StatusDaVenda != StatusDaVenda.Entregue)
                {
                    throw new Exception("A venda esta com o status de Enviar para Transportadora, por favor escolha 'Entrgue'!");
                }
            }
            var toVendaModel = _adapter.ToVendaModelUpdate(vendaRequestUpdateDTO);

            var vendaAtualizada = await _repository.AtualizarVenda(id, toVendaModel);
            return await BuscarVendaPorId(vendaAtualizada.Id);
        }
    }
}
