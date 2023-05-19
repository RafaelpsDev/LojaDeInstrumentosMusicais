using AutoFixture;
using LojaDeInstrumentosMusicais.Application.Adapters;
using LojaDeInstrumentosMusicais.Application.DTOs;
using LojaDeInstrumentosMusicais.Application.Interfaces;
using LojaDeInstrumentosMusicais.Domain.Models;
using LojaDeInstrumentosMusicais.Tests.Unit.Utils;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LojaDeInstrumentosMusicais.Tests.Unit.Adapters
{
    public class VendedorAdaptersTests
    {
        private readonly Fixture _fixture;
        private readonly Mock<IVendedorAdapter> _vendedorAdapterMock;
        private readonly Mock<IVendaAdapter> _vendaAdapterMock;
        private readonly Mock<IInstrumentoMusicalAdapter> _instrumentoMusicalAdapterMock;

        public VendedorAdaptersTests()
        {
            _fixture = new Fixture();
            _fixture.OmitirComportamentoRecursivo();
            _vendedorAdapterMock = new Mock<IVendedorAdapter>();
        }

        [Fact]
        public void ToVendedorResponse_Test_AdaptacaoCorreta()
        {
            var vendedorModel = _fixture.Create<VendedorModel>();
            var vendedorResponse = new VendedorResponseDTO
            {
                Nome = vendedorModel.Nome,
                Cpf = vendedorModel.Cpf,
                Email = vendedorModel.Email,
                Telefone = vendedorModel.Telefone
            };
            var adapter = new VendedorAdapter();


            var resultado = adapter.ToVendedorResponseDTO(vendedorModel);

            resultado.Nome.ShouldBe(vendedorResponse.Nome);
            resultado.Cpf.ShouldBe(vendedorResponse.Cpf);
            resultado.Email.ShouldBe(vendedorResponse.Email);
            resultado.Telefone.ShouldBe(vendedorResponse.Telefone);
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

            var adapter = new VendedorAdapter();
            var retorno = adapter.ToVendedorModel(vendedorRequest);

            retorno.Nome.ShouldBe(vendedorModel.Nome);
            retorno.Cpf.ShouldBe(vendedorModel.Cpf);
            retorno.Email.ShouldBe(vendedorModel.Email);
            retorno.Telefone.ShouldBe(vendedorRequest.Telefone);
        }
    }
}
