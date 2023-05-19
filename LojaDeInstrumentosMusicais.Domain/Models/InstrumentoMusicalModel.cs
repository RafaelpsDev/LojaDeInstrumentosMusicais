using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LojaDeInstrumentosMusicais.Domain.Models
{
    public class InstrumentoMusicalModel
    {
        public int Id { get; set; }
        public int IdVenda { get; set; }
        public string Nome { get; set; }
        public double Valor { get; set; }
        [JsonIgnore]
        public VendaModel Venda { get; set; }
    }
}
