using LojaDeInstrumentosMusicais.Domain.Enums;
using LojaDeInstrumentosMusicais.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LojaDeInstrumentosMusicais.Application.DTOs
{
    public class VendaRequestDTO
    {
        public ICollection<InstrumentoMusicalRequestDTO> Instrumentos { get; set; }
        public VendedorRequestDTO Vendedor { get; set; }
    }
}
