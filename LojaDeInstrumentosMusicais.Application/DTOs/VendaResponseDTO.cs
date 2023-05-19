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
    public class VendaResponseDTO
    {
        public int Id { get; set; }
        public Guid IdPedido { get; set; }
        public int IdVendedor { get; set; }
        public DateTime DataDaVenda { get; set; }
        public StatusDaVenda StatusDaVenda { get; set; }       
        public ICollection<InstrumentoMusicalResponseDTO> Instrumentos { get; set; }        
        public VendedorResponseDTO Vendedor { get; set; }
    }
}
