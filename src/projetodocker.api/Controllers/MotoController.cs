using Microsoft.AspNetCore.Mvc;
using Application;
using Domain.Entity;
using MassTransit;
using MassTransit.Transports;
using Shared.Messages;
using Application.Command;

namespace API
{
    [Route("motos")]
    public class MotoController : ControllerBaseLocal
    {
        /// <summary>
        /// Lista todas as motos.
        /// </summary>
        [HttpGet, Route("")]
        public async Task<IActionResult> GetMotos([FromQuery] MotoQuery request)
        {
            return Ok(Mediator.Send(request).Result.Content);
        }

        /// <summary>
        /// Obtém uma moto pelo ID.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMotoById(int id)
        {
            MotoQueryById request = new MotoQueryById(id);
            return Ok(Mediator.Send(request).Result.Content);
        }

        /// <summary>
        /// Cria uma nova moto.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> CreateMoto([FromBody] MotoCriarComand request)
        {
            return Ok(Mediator.Send(request).Result.Content);
        }

        /// <summary>
        /// Atualiza os dados de uma moto existente.
        /// </summary>
        [HttpPut("{id}/placa")]
        public async Task<IActionResult> UpdateMoto([FromRoute] int id, [FromBody] ModificarPlacaMotoDto placaDto)
        {
            MotoAtualizarComand request = new MotoAtualizarComand(id, placaDto.placa);
            return Ok(Mediator.Send(request).Result.Content);
        }

        /// <summary>
        /// Exclui uma moto pelo ID.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMoto(int id)
        {
            MotoRemoverComand request = new MotoRemoverComand(id);
            return Ok(Mediator.Send(request).Result.Content);
        }
    }
}