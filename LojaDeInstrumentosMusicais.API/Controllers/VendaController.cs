using LojaDeInstrumentosMusicais.Application.DTOs;
using LojaDeInstrumentosMusicais.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LojaDeInstrumentosMusicais.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VendaController : ControllerBase
    {
        private readonly IVendaService _vendaService;
        public VendaController(IVendaService vendaService)
        {
            _vendaService = vendaService;
        }
        [HttpGet]
        [Route("/BuscarVendaPorId")]
        public async Task<ActionResult> BuscarVendaPorId(int id)
        {
            var venda = await _vendaService.BuscarVendaPorId(id);
            return Ok(venda);
        }
        [HttpPost]
        [Route("/RegistrarVenda")]
        public async Task<ActionResult> RegistrarVenda(VendaRequestDTO vendaRequestDTO)
        {
            var venda = await _vendaService.RegistrarVenda(vendaRequestDTO);
            return Ok(venda);
        }
        [HttpPut]
        [Route("/AtualizarStatusDeVenda")]
        public async Task<ActionResult> AtualizarStatusDeVenda(int id, VendaRequestUpdateDTO vendaRequestUpdateDTO)
        {
            var vendaUpdate = await _vendaService.AtualizarVenda(id, vendaRequestUpdateDTO);
            return Ok(vendaUpdate);
        }
    }
}
