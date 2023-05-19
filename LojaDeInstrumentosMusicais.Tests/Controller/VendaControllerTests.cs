using AutoFixture;
using LojaDeInstrumentosMusicais.API.Controllers;
using LojaDeInstrumentosMusicais.Application.DTOs;
using LojaDeInstrumentosMusicais.Application.Interfaces;
using LojaDeInstrumentosMusicais.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LojaDeInstrumentosMusicais.Tests.Unit.Controller
{
    public class VendaControllerTests
    {
        private readonly Fixture _fixture;
        private readonly Mock<IVendaService> _serviceMock;
        public VendaControllerTests()
        {
            _fixture = new Fixture();
            _serviceMock = new Mock<IVendaService>();            
        }
        [Fact]
        public async Task BuscarVendaPorIdController_Test()
        {
            var controller = new VendaController(_serviceMock.Object);

            var id = _fixture.Create<int>();
            var vendaResponse = _fixture.Create<VendaResponseDTO>();

             _serviceMock.Setup(x => x.BuscarVendaPorId(It.IsAny<int>())).Returns(Task.FromResult(vendaResponse));
            var retorno = await controller.BuscarVendaPorId(id);


            Assert.NotNull(retorno);
            Assert.IsType<OkObjectResult>(retorno);
        }
        [Fact]
        public async Task RegistrarVendaController_Test()
        {
            var controller = new VendaController(_serviceMock.Object);
            var vendaRequest = _fixture.Create<VendaRequestDTO>();
            var vendaResponse = _fixture.Create<VendaResponseDTO>();

            _serviceMock.Setup(v => v.RegistrarVenda(vendaRequest)).Returns(Task.FromResult(vendaResponse));
            var retorno = await controller.RegistrarVenda(vendaRequest);

            Assert.NotNull(retorno);
            Assert.IsType<OkObjectResult>(retorno);

        }
        [Fact]
        public async Task AtualizarStatusDeVenda_Test()
        {
            var controller = new VendaController(_serviceMock.Object);
            var id = _fixture.Create<int>();
            var vendaRequest = _fixture.Create<VendaRequestUpdateDTO>();
            var vendaResponse = _fixture.Create<VendaResponseDTO>();
            _serviceMock.Setup(v => v.AtualizarVenda(id, vendaRequest)).Returns(Task.FromResult(vendaResponse));

            var retorno = await controller.AtualizarStatusDeVenda(id, vendaRequest);
            Assert.NotNull(retorno);
            Assert.IsType<OkObjectResult>(retorno);

        }
    }
}
