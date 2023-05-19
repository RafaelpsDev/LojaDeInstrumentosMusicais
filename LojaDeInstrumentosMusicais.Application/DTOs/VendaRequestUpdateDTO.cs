using LojaDeInstrumentosMusicais.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LojaDeInstrumentosMusicais.Application.DTOs
{
    public class VendaRequestUpdateDTO
    {
        public DateTime? DataDeAlteracao { get; set; } = DateTime.Now;
        public StatusDaVenda StatusDaVenda { get; set; }

    }
}
