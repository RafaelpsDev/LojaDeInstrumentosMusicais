using AutoFixture;
using LojaDeInstrumentosMusicais.Application.DTOs;
using LojaDeInstrumentosMusicais.Application.Interfaces;
using LojaDeInstrumentosMusicais.Application.Services;
using LojaDeInstrumentosMusicais.Domain.Enums;
using LojaDeInstrumentosMusicais.Domain.Models;
using LojaDeInstrumentosMusicais.Tests.Unit.Utils;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LojaDeInstrumentosMusicais.Tests.Unit.Services
{
    public class VendaServiceTests
    {
        private readonly Fixture _fixture;
        private readonly Mock<IVendaRepository> _vendaRepositoryMock;
        private readonly Mock<IVendaAdapter> _vendaAdapterMock;
        public VendaServiceTests()
        {
            _fixture = new Fixture();
            _fixture.OmitirComportamentoRecursivo();
            _vendaRepositoryMock = new Mock<IVendaRepository>();
            _vendaAdapterMock = new Mock<IVendaAdapter>();
        }

        [Fact]
        public async Task RegistrarVendaTestSucesso()
        {
            //Arrange

            //Objeto utilizado para inserir um vendedor
            var vendaRequestDTO = _fixture.Create<VendaRequestDTO>();
            var venda = _fixture.Create<VendaModel>();
            //Objeto de retorno
            var vendaResponseDTO = _fixture.Create<VendaResponseDTO>();

            //Adaptando o objeto VendedorRequestDTO para VendedorModel
            _vendaAdapterMock.Setup(x => x.ToVendaModel(vendaRequestDTO))
                .Returns(venda);

            //Adaptando o objeto VendedorModel para VendedorResponseDTO
            _vendaAdapterMock.Setup(x => x.ToVendaResponse(venda))
                .Returns(vendaResponseDTO);

            //Act
            //Setando o repositório
            _vendaRepositoryMock.Setup(x => x.RegistrarVenda(It.IsAny<VendaModel>())).Returns(Task.FromResult(venda));
            //Carregando a variavel do serviço
            var service = new VendaService(_vendaRepositoryMock.Object, _vendaAdapterMock.Object);
            //executando o serviço AdicionarVendedor
            var responseRegistrarVenda = await service.RegistrarVenda(vendaRequestDTO);
            //Assert
            responseRegistrarVenda.Id.ShouldBe(vendaResponseDTO.Id);
            responseRegistrarVenda.IdPedido.ShouldBe(vendaResponseDTO.IdPedido);
            responseRegistrarVenda.DataDaVenda.ShouldBe(vendaResponseDTO.DataDaVenda);
            responseRegistrarVenda.StatusDaVenda.ShouldBe(vendaResponseDTO.StatusDaVenda);
            responseRegistrarVenda.Instrumentos.ShouldBe(vendaResponseDTO.Instrumentos);
            responseRegistrarVenda.Vendedor.ShouldBe(vendaResponseDTO.Vendedor);
            _vendaRepositoryMock.Verify(vr => vr.RegistrarVenda(It.IsAny<VendaModel>()), Times.Once);
        }

        [Fact]
        public async Task BuscarVendaPorIdSucesso()
        {
            var venda = _fixture.Create<VendaModel>();
            var vendaResponseDTO = _fixture.Create<VendaResponseDTO>();
            var idVenda = _fixture.Create<int>();

            _vendaRepositoryMock.Setup(x => x.BuscarVendaPorId(idVenda)).Returns(Task.FromResult(venda));

            _vendaAdapterMock.Setup(x => x.ToVendaResponse(venda))
                .Returns(vendaResponseDTO);
            var service = new VendaService(_vendaRepositoryMock.Object, _vendaAdapterMock.Object);

            var retorno = await service.BuscarVendaPorId(idVenda);

            Assert.NotNull(retorno);
            Assert.IsType<VendaResponseDTO>(retorno);
        }
        [Fact]
        public async Task BuscarVendaPorIdException()
        {
            var idVenda = _fixture.Create<int>();

            var service = new VendaService(_vendaRepositoryMock.Object, _vendaAdapterMock.Object);

            var execption = await Assert.ThrowsAsync<Exception>(async () => await service.BuscarVendaPorId(idVenda));
            Assert.Equal("Por favor, informar um Id de venda válido.", execption.Message);
        }
        [Theory]
        [InlineData(StatusDaVenda.AguardandoPagamento, StatusDaVenda.PagamentoAprovado)]
        [InlineData(StatusDaVenda.AguardandoPagamento, StatusDaVenda.Cancelado)]
        [InlineData(StatusDaVenda.PagamentoAprovado, StatusDaVenda.EnviadoParaTransportadora)]
        [InlineData(StatusDaVenda.PagamentoAprovado, StatusDaVenda.Cancelado)]        
        [InlineData(StatusDaVenda.EnviadoParaTransportadora, StatusDaVenda.Entregue)]
        public async Task AtualizarVendaService_Test_AtualizacaoSucesso(StatusDaVenda de, StatusDaVenda para)
        {
            var venda = _fixture.Build<VendaModel>().With(vm => vm.StatusDaVenda, de).Create();
            var vendaRequest = _fixture.Build<VendaRequestUpdateDTO>().With(vr => vr.StatusDaVenda, para).Create();
            var vendaResponse = _fixture.Create<VendaResponseDTO>();
            vendaResponse.StatusDaVenda = venda.StatusDaVenda;
            var id = venda.Id;

            _vendaAdapterMock.Setup(x => x.ToVendaModelUpdate(vendaRequest))
                .Returns(venda);

            _vendaAdapterMock.Setup(x => x.ToVendaResponse(venda))
                .Returns(vendaResponse);


            _vendaRepositoryMock.Setup(x => x.BuscarVendaPorId(id)).Returns(Task.FromResult(venda));

            _vendaRepositoryMock.Setup(x => x.AtualizarVenda(id, venda)).Returns(Task.FromResult(venda));

            var service = new VendaService(_vendaRepositoryMock.Object, _vendaAdapterMock.Object);
            var retornoAtualizacaoDeVenda = await service.AtualizarVenda(id, vendaRequest);
                        
            Assert.NotNull(retornoAtualizacaoDeVenda);
            Assert.IsType<VendaResponseDTO>(retornoAtualizacaoDeVenda);
        }

        [Theory]
        [InlineData(StatusDaVenda.AguardandoPagamento, StatusDaVenda.EnviadoParaTransportadora)]
        [InlineData(StatusDaVenda.AguardandoPagamento, StatusDaVenda.Entregue)]
        [InlineData(StatusDaVenda.PagamentoAprovado, StatusDaVenda.Entregue)]
        [InlineData(StatusDaVenda.PagamentoAprovado, StatusDaVenda.AguardandoPagamento)]
        [InlineData(StatusDaVenda.EnviadoParaTransportadora, StatusDaVenda.AguardandoPagamento)]
        [InlineData(StatusDaVenda.EnviadoParaTransportadora, StatusDaVenda.PagamentoAprovado)]
        [InlineData(StatusDaVenda.EnviadoParaTransportadora, StatusDaVenda.Cancelado)]
        public async Task AtualizarVendaService_Test_AtualizacaoNaoPermitida(StatusDaVenda de, StatusDaVenda para)
        {
            var venda = _fixture.Build<VendaModel>().With(vm => vm.StatusDaVenda, de).Create();
            var vendaRequest = _fixture.Build<VendaRequestUpdateDTO>().With(vr => vr.StatusDaVenda, para).Create();
            var vendaResponse = _fixture.Create<VendaResponseDTO>();
            vendaResponse.StatusDaVenda = venda.StatusDaVenda;
            var id = venda.Id;


            _vendaAdapterMock.Setup(x => x.ToVendaModelUpdate(vendaRequest))
                .Returns(venda);

            _vendaAdapterMock.Setup(x => x.ToVendaResponse(venda))
                .Returns(vendaResponse);


            _vendaRepositoryMock.Setup(x => x.BuscarVendaPorId(id)).Returns(Task.FromResult(venda));

            _vendaRepositoryMock.Setup(x => x.AtualizarVenda(id, venda)).Returns(Task.FromResult(venda));

            var service = new VendaService(_vendaRepositoryMock.Object, _vendaAdapterMock.Object);
            
            await Assert.ThrowsAsync<Exception>(async () => await service.AtualizarVenda(id, vendaRequest));
        }

        [Fact]
        public async Task AtualizarVendaService_Test_Exception()
        {
            var venda = _fixture.Create<VendaModel>();
            var vendaRequest = _fixture.Create<VendaRequestUpdateDTO>();
            var vendaResponse = _fixture.Create<VendaResponseDTO>();
            vendaResponse.StatusDaVenda = venda.StatusDaVenda;
            var id = _fixture.Create<int>();

            _vendaAdapterMock.Setup(x => x.ToVendaModelUpdate(vendaRequest))
                .Returns(venda);

            _vendaAdapterMock.Setup(x => x.ToVendaResponse(venda))
                .Returns(vendaResponse);

            _vendaRepositoryMock.Setup(x => x.BuscarVendaPorId(id)).Returns(Task.FromResult(venda));

            _vendaRepositoryMock.Setup(x => x.AtualizarVenda(id, venda)).Returns(Task.FromResult(venda));

            var service = new VendaService(_vendaRepositoryMock.Object, _vendaAdapterMock.Object);

            var execption = await Assert.ThrowsAsync<Exception>(async () => await service.AtualizarVenda(id, vendaRequest));
            Assert.Equal("Por favor, informar um Id de venda válido.", execption.Message);
        }
    }
}
