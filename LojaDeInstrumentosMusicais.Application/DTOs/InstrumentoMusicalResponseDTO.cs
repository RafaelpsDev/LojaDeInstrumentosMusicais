using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LojaDeInstrumentosMusicais.Application.DTOs
{
    public class InstrumentoMusicalResponseDTO
    {
        public int Id { get; set; }
        public int IdVenda { get; set; }
        public string Nome { get; set; }
        public double Valor { get; set; }
    }
}
