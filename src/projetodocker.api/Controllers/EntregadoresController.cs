using Microsoft.AspNetCore.Mvc;
using Application;
using Domain.Entity;
using Application.Command;

namespace API
{
    [Route("entregadores")]
    public class EntregadoresController : ControllerBaseLocal
    {
        [HttpPost]
        public async Task<IActionResult> CadastrarEntregador([FromBody] EntregadorCriarComand novoEntregador)
        {
            return Ok(Mediator.Send(novoEntregador).Result.Content);
        }

        [HttpPost("{id}/cnh")]
        public async Task<IActionResult> EnviarFotoCNH([FromQuery] int id, [FromBody] EntregadorAtualizarFotoDto imagemCNH)
        {
            EntregadorFotoCriarComand request = new EntregadorFotoCriarComand(id, imagemCNH.imagem_cnh);
            return Ok( Mediator.Send(request).Result.Content);
        }

        /// <summary>
        /// Lista todas os entregadores.
        /// </summary>
        [HttpGet, Route("")]
        public async Task<IActionResult> GetEntregadores([FromQuery] EntregadorQuery request)
        {
            return Ok( Mediator.Send(request).Result.Content);
        }
    }
}