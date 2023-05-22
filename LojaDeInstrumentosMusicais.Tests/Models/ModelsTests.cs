using AutoFixture;
using LojaDeInstrumentosMusicais.Domain.Models;
using LojaDeInstrumentosMusicais.Tests.Unit.Utils;

namespace LojaDeInstrumentosMusicais.Tests.Unit.Models
{
    public class ModelsTests
    {
        private readonly Fixture _fixture;
        public ModelsTests()
        {
            _fixture = new Fixture();
            _fixture.OmitirComportamentoRecursivo();
        }
        [Fact]
        public void VendedorModel_Test()
        {
            var venda = _fixture.CreateMany<VendaModel>(2).ToList();

            var vendedor = _fixture.Build<VendedorModel>()
                .With(x => x.Vendas, venda)
                .Create();

            var relacionamentos = new List<object>
            {
                vendedor.Vendas
            };

            Assert.DoesNotContain(null, relacionamentos);
        }
        [Fact]
        public void VendaModel_Test()
        {
            var vendedor = _fixture.Create<VendedorModel>();
            var instrumentosMusicais = _fixture.CreateMany<InstrumentoMusicalModel>(3).ToList();


            var venda = _fixture.Build<VendaModel>()
                .With(x => x.Vendedor, vendedor)
                .With(x => x.Instrumentos, instrumentosMusicais)
                .Create();

            var relacionamentos = new List<object>
            {
                venda.Vendedor,
                venda.Instrumentos
            };

            Assert.DoesNotContain(null, relacionamentos);
        }
        [Fact]
        public void InstrumentoMusicalModel_Test()
        {
            var venda = _fixture.Create<VendaModel>();
            
            var instrumentoMusical = _fixture.Build<InstrumentoMusicalModel>()
                .With(i => i.Venda, venda)
                .Create();

            var relacionamentos = new List<object>
            {
                instrumentoMusical.Venda
            };

            Assert.DoesNotContain(null, relacionamentos);
        }
    }
}
