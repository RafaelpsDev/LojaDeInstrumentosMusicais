using LojaDeInstrumentosMusicais.Application.DTOs;
using LojaDeInstrumentosMusicais.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LojaDeInstrumentosMusicais.Application.Interfaces
{
    public interface IInstrumentoMusicalAdapter
    {
        public ICollection<InstrumentoMusicalModel> ToInstrumentoMusicalModel(ICollection<InstrumentoMusicalRequestDTO> instrumentoMusicalRequestDTO);
        public ICollection<InstrumentoMusicalResponseDTO> ToInstrumentoMusicalResponse(ICollection<InstrumentoMusicalModel> instrumentoMusicalModel);
    }
}
