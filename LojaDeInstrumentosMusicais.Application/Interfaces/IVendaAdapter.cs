using LojaDeInstrumentosMusicais.Application.DTOs;
using LojaDeInstrumentosMusicais.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LojaDeInstrumentosMusicais.Application.Interfaces
{
    public interface IVendaAdapter
    {
        public VendaModel ToVendaModel(VendaRequestDTO vendaRequestDTO);
        public VendaModel ToVendaModelUpdate(VendaRequestUpdateDTO vendaRequestUpdateDTO);
        public VendaResponseDTO ToVendaResponse(VendaModel vendaModel);
    }
}
