using Application;
using Application.Command;
using Domain.Entity;
using Microsoft.AspNetCore.Mvc;

namespace API
{
    [Route("locacao")]
    public class LocacaoController : ControllerBaseLocal
    {

        /// <summary>
        /// Cria uma nova locação.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> CreateLocacao([FromBody] LocacaoCriarComand novaLocacao)
        {
            return Ok(Mediator.Send(novaLocacao).Result.Content);
        }

        /// <summary>
        /// Obtém uma locação pelo ID.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetLocacaoById(int id)
        {
            LocacaoQueryById request = new LocacaoQueryById(id);
            return Ok(Mediator.Send(request).Result.Content);
        }


        /// <summary>
        /// Atualiza a DataTermino de uma locação.
        /// </summary>
        [HttpPut("{id}/devolucao")]
        public async Task<IActionResult> ConcluirLocacao([FromQuery] int id, [FromBody] FinalizaLocadaoDto finalizaLocacao)
        {
            LocacaoDevolucaoComand request = new LocacaoDevolucaoComand(id, finalizaLocacao.data_devolucao);
            return Ok( Mediator.Send(request).Result.Content);
        }


    }
}