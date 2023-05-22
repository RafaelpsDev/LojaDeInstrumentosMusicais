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
    public class VendaAdapterTests
    {
        private readonly VendaAdapter _adapter;
        private readonly Fixture _fixture;
        private readonly Mock<IInstrumentoMusicalAdapter> _instrumentoMusicalAdapterMock;
        private readonly Mock<IVendedorAdapter> _vendedorAdapterMock;
        public VendaAdapterTests()
        {
            _fixture = new Fixture();
            _fixture.OmitirComportamentoRecursivo();
            _instrumentoMusicalAdapterMock = new Mock<IInstrumentoMusicalAdapter>();
            _vendedorAdapterMock = new Mock<IVendedorAdapter>();
            _adapter = new VendaAdapter(_vendedorAdapterMock.Object, _instrumentoMusicalAdapterMock.Object);
        }

        [Fact]
        public void ToVendaModelAdapter_Test_AdaptacaoCorreta()
        {
            //Arrange
            var vendedorRequest = _fixture.Create<VendedorRequestDTO>();
            var vendedorModel = new VendedorModel()
            {
                Nome = vendedorRequest.Nome,
                Cpf = vendedorRequest.Cpf,
                Email = vendedorRequest.Email,
                Telefone = vendedorRequest.Telefone
            };
            var instrumentoMusicalModel = new List<InstrumentoMusicalModel>();
            var instrumentoMusicalRequest = _fixture.Create<List<InstrumentoMusicalRequestDTO>>();

            foreach (var dto in instrumentoMusicalRequest)
            {
                var model = new InstrumentoMusicalModel
                {
                    Nome = dto.Nome,
                    Valor = dto.Valor
                };

                instrumentoMusicalModel.Add(model);
            }
            var vendaRequest = new VendaRequestDTO()
            {
                Vendedor = vendedorRequest,
                Instrumentos = instrumentoMusicalRequest
            };

            _vendedorAdapterMock.Setup(x => x.ToVendedorModel(vendedorRequest)).Returns(vendedorModel);
            _instrumentoMusicalAdapterMock.Setup(x => x.ToInstrumentoMusicalModel(instrumentoMusicalRequest))
                .Returns(instrumentoMusicalModel);
            //Act
            var toVendaModel = _adapter.ToVendaModel(vendaRequest);

            // Assert
            Assert.Equal(vendedorModel, toVendaModel.Vendedor);
            Assert.Equal(instrumentoMusicalModel, toVendaModel.Instrumentos);
        }

        [Fact]
        public void ToVendaModelUpdateAdapter_Test_AdaptacaoCorreta()
        {
            //Arrange
            var vendaRequestUpdate = _fixture.Create<VendaRequestUpdateDTO>();
            var vendaModel = new VendaModel()
            {                
                DataDeAlteracao = vendaRequestUpdate.DataDeAlteracao,
                StatusDaVenda = vendaRequestUpdate.StatusDaVenda
            };

            //Act
            var retorno = _adapter.ToVendaModelUpdate(vendaRequestUpdate);

            //Assert
            vendaModel.DataDeAlteracao.ShouldBe(retorno.DataDeAlteracao);
            vendaModel.StatusDaVenda.ShouldBe(retorno.StatusDaVenda);
        }
        [Fact]
        public void ToVendaResponse_Test_AdaptacaoCorreta()
        {
            //Arrange
            var vendedorModel = _fixture.Create<VendedorModel>();
            var vendedorResponse = new VendedorResponseDTO()
            {
                Id = vendedorModel.Id,
                Nome = vendedorModel.Nome,
                Cpf = vendedorModel.Cpf,
                Email = vendedorModel.Email,
                Telefone = vendedorModel.Telefone
            };
            var instrumentoMusicalModel = _fixture.Create<List<InstrumentoMusicalModel>>();
            var instrumentoMusicalResponse = new List<InstrumentoMusicalResponseDTO>();

            foreach (var model in instrumentoMusicalModel)
            {
                var response = new InstrumentoMusicalResponseDTO
                {
                    Id = model.Id,
                    IdVenda = model.IdVenda,
                    Nome = model.Nome,
                    Valor = model.Valor
                };
                instrumentoMusicalResponse.Add(response);
            }
            var venda = _fixture.Create<VendaModel>();
            var vendaModel = new VendaModel
            {
                Id = venda.Id,
                IdPedido = venda.IdPedido,
                IdVendedor = venda.IdVendedor,
                DataDaVenda = venda.DataDaVenda,
                StatusDaVenda = venda.StatusDaVenda,
                Vendedor = vendedorModel,
                Instrumentos = instrumentoMusicalModel
            };
            var vendaResponse = new VendaResponseDTO
            {
                Id = vendaModel.Id,
                IdPedido = vendaModel.IdPedido,
                IdVendedor = vendaModel.IdVendedor,
                DataDaVenda = vendaModel.DataDaVenda,
                StatusDaVenda = vendaModel.StatusDaVenda,
                Vendedor = vendedorResponse,
                Instrumentos = instrumentoMusicalResponse
            };
            
            _instrumentoMusicalAdapterMock.Setup(ima => ima.ToInstrumentoMusicalResponse(instrumentoMusicalModel))
                .Returns(instrumentoMusicalResponse);
            _vendedorAdapterMock.Setup(vam => vam.ToVendedorResponseDTO(vendedorModel))
                .Returns(vendedorResponse);

            //Act
            var retorno = _adapter.ToVendaResponse(vendaModel);

            // Assert
            vendaResponse.ShouldBeEquivalentTo(retorno);
            vendedorResponse.ShouldBe(retorno.Vendedor);
            instrumentoMusicalResponse.ShouldBe(retorno.Instrumentos);
        }

    }
}
