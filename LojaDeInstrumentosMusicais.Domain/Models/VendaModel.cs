using LojaDeInstrumentosMusicais.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LojaDeInstrumentosMusicais.Domain.Models
{
    public class VendaModel
    {
        public int Id { get; set; }
        public Guid IdPedido { get; set; } = Guid.NewGuid();
        public int IdVendedor { get; set; }
        public DateTime DataDaVenda { get; set; } = DateTime.Now;
        public DateTime? DataDeAlteracao { get; set; } = null;
        public StatusDaVenda StatusDaVenda { get; set; }
        [JsonIgnore]
        public ICollection<InstrumentoMusicalModel> Instrumentos { get; set; }
        [JsonIgnore]
        public VendedorModel Vendedor { get; set; }
    }
}
