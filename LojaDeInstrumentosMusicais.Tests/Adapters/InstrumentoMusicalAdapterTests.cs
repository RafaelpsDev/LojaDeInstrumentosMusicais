using AutoFixture;
using LojaDeInstrumentosMusicais.Application.Adapters;
using LojaDeInstrumentosMusicais.Application.DTOs;
using LojaDeInstrumentosMusicais.Application.Interfaces;
using LojaDeInstrumentosMusicais.Domain.Models;
using LojaDeInstrumentosMusicais.Tests.Unit.Utils;
using Moq;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LojaDeInstrumentosMusicais.Tests.Unit.Adapters
{
    public class InstrumentoMusicalAdapterTests
    {
        private readonly Fixture _fixture;        
        private readonly Mock<IInstrumentoMusicalAdapter> _adapterMock;
        public InstrumentoMusicalAdapterTests()
        {
            _fixture = new Fixture();
            _fixture.OmitirComportamentoRecursivo();
            _adapterMock = new Mock<IInstrumentoMusicalAdapter>();
            
        }

        [Fact]
        public void ToInstrumentoMusicalModel_Test_AdaptacaoCorreta()
        {
            //arrange
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
            var adapter = new InstrumentoMusicalAdapter();
            //Act
            var retorno = adapter.ToInstrumentoMusicalModel(instrumentoMusicalRequest);
            var retornoList = new List<InstrumentoMusicalModel>(retorno);
            // Assert
            Assert.NotNull(retorno);
            Assert.NotNull(retornoList);
            Assert.Equal(instrumentoMusicalRequest.Count, retornoList.Count);

            for (int i = 0; i < instrumentoMusicalRequest.Count; i++)
            {
                Assert.Equal(instrumentoMusicalRequest[i].Nome, retornoList[i].Nome);
                Assert.Equal(instrumentoMusicalRequest[i].Valor, retornoList[i].Valor);
            }
        }

        [Fact]
        public void ToInstrumentoMusicalResponse_Test_AdaptacaoCorreta()
        {
            //Arrange
            var instrumentoMusicalResponse = new List<InstrumentoMusicalResponseDTO>();
            var instrumentoMusicalModel = _fixture.Create<List<InstrumentoMusicalModel>>();

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
            var adapter = new InstrumentoMusicalAdapter();

            //Act
            var retorno = adapter.ToInstrumentoMusicalResponse(instrumentoMusicalModel);
            var retornoList = new List<InstrumentoMusicalResponseDTO>(retorno);

                       
            //Assert
            Assert.Equal(retorno.Count, instrumentoMusicalModel.Count);
            for (int i = 0; i < instrumentoMusicalModel.Count; i++)
            {
                Assert.Equal(instrumentoMusicalModel[i].Id, retornoList[i].Id); // Verifica se os Ids correspondem
                Assert.Equal(instrumentoMusicalModel[i].IdVenda, retornoList[i].IdVenda); // Verifica se os IdVenda's correspondem
                Assert.Equal(instrumentoMusicalModel[i].Nome, retornoList[i].Nome); // Verifica se os nomes correspondem
                Assert.Equal(instrumentoMusicalModel[i].Valor, retornoList[i].Valor); // Verifica se os valores correspondem
            }
        }
    }
}
