using AutoFixture;
using LojaDeInstrumentosMusicais.Application.Adapters;
using LojaDeInstrumentosMusicais.Application.DTOs;
using LojaDeInstrumentosMusicais.Application.Interfaces;
using LojaDeInstrumentosMusicais.Domain.Models;
using LojaDeInstrumentosMusicais.Tests.Unit.Utils;
using Moq;
using Shouldly;

namespace LojaDeInstrumentosMusicais.Tests.Unit.Adapters
{
    public class VendedorAdaptersTests
    {
        private readonly Fixture _fixture;
        private readonly VendedorAdapter _adapter;

        public VendedorAdaptersTests()
        {

            _fixture = new Fixture();
            _fixture.OmitirComportamentoRecursivo();
            _adapter = new VendedorAdapter();
        }

        [Fact]
        public void ToVendedorResponse_Test_AdaptacaoCorreta()
        {
            var vendedorModel = _fixture.Create<VendedorModel>();
            var vendedorResponse = new VendedorResponseDTO
            {
                Id = vendedorModel.Id,
                Nome = vendedorModel.Nome,
                Cpf = vendedorModel.Cpf,
                Email = vendedorModel.Email,
                Telefone = vendedorModel.Telefone
            };
            
            var retorno = _adapter.ToVendedorResponseDTO(vendedorModel);
            
            retorno.ShouldBeEquivalentTo(vendedorResponse);
        }
        [Fact]
        public void ToVendedorModel_Test_AdaptacaoCorreta()
        {
            var vendedorRequest = _fixture.Create<VendedorRequestDTO>();
            var vendedorModel = new VendedorModel
            {
                Nome = vendedorRequest.Nome,
                Cpf = vendedorRequest.Cpf,
                Email = vendedorRequest.Email,
                Telefone = vendedorRequest.Telefone
            };

            var retorno = _adapter.ToVendedorModel(vendedorRequest);

            retorno.ShouldBeEquivalentTo(vendedorModel);
        }
    }
}
