using LojaDeInstrumentosMusicais.Application.DTOs;
using LojaDeInstrumentosMusicais.Application.Interfaces;
using LojaDeInstrumentosMusicais.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LojaDeInstrumentosMusicais.Application.Adapters
{
    public class InstrumentoMusicalAdapter : IInstrumentoMusicalAdapter
    {
        public ICollection<InstrumentoMusicalModel> ToInstrumentoMusicalModel(ICollection<InstrumentoMusicalRequestDTO> instrumentoMusicalRequestDTO)
        {
            var models = new List<InstrumentoMusicalModel>();

            foreach (var dto in instrumentoMusicalRequestDTO)
            {
                var model = new InstrumentoMusicalModel
                {
                    Nome = dto.Nome,
                    Valor = dto.Valor
                };

                models.Add(model);
            }

            return models;
        }

        public ICollection<InstrumentoMusicalResponseDTO> ToInstrumentoMusicalResponse(ICollection<InstrumentoMusicalModel> instrumentoMusicalModel)
        {
            var instrumentoResponse = new List<InstrumentoMusicalResponseDTO>();
            foreach (var model in instrumentoMusicalModel)
            {
                var response = new InstrumentoMusicalResponseDTO
                {
                    Id = model.Id,
                    IdVenda = model.IdVenda,
                    Nome = model.Nome,
                    Valor = model.Valor
                };
                instrumentoResponse.Add(response);                
            }
            return instrumentoResponse;
        }
    }
}
