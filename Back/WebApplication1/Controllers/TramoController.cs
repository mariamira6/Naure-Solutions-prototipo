using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting.Internal;
using System.Net;
using NaureBack.Converters;
using NaureBack.DTOs;
using NaureBack.Models;
using NaureBack.Services;

namespace NaureBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TramoController : ControllerBase
    {
        private readonly NaureContext _context;
        private readonly PalabraClaveServicio _palabraClave;

        public TramoController(NaureContext context, PalabraClaveServicio palabraClave)
        {
            _context = context;
            _palabraClave = palabraClave;
        }

        /// <summary>
        /// Listar todos los tramos de la BBDD
        /// Se incluyen los datos de la contratación asociada a cada tramo
        /// </summary>
        /// <returns>Lista con los tramos de la BBDD</returns>
        [HttpGet("ListarTramos")]
        public async Task<ActionResult<IList<TramoDTO>>> ListarTramosAsync()
        {
            IList<TramoDTO>? res = null;

            if (Request.Headers.ContainsKey("Token") && _palabraClave.Existe(Request.Headers["Token"].ToString()))
            {
                TramoServicio tramoServicio = new TramoServicio(_context);
                IList<Tramo> tramos = await tramoServicio.ListarTodosTramosAsync();
                TramoConversor conversor;

                if (tramos != null)
                {
                    conversor = new TramoConversor(tramos);
                    res = conversor.ListaTramoDTO;
                }

                return StatusCode(StatusCodes.Status200OK, res);
            }
            else
            {
                return StatusCode(StatusCodes.Status401Unauthorized, res);
            }
        }

        /// <summary>
        /// Listar todos los tramos relacionados con la contratación "idContratacion"
        /// Se incluyen los datos de la contratación asociada a cada tramo
        /// </summary>
        /// <param name="idContratacion">Identificador de la contratación para buscar los tramos</param>
        /// <returns>Lista de tramos de la contratación</returns>
        [HttpGet("ListarTramosContratacion")]
        public async Task<ActionResult<IList<TramoDTO>>> ListarTramosContratacionAsync(int idContratacion)
        {
            IList<TramoDTO>? res = null;

            if (Request.Headers.ContainsKey("Token") && _palabraClave.Existe(Request.Headers["Token"].ToString()))
            {
                TramoServicio tramoServicio = new TramoServicio(_context);
                IList<Tramo> tramos = await tramoServicio.ListarTramosContratacionAsync(idContratacion);
                TramoConversor conversor;

                if (tramos != null)
                {
                    conversor = new TramoConversor(tramos);
                    res = conversor.ListaTramoDTO;
                    return StatusCode(StatusCodes.Status200OK, res);
                }
                else
                {
                    return StatusCode(StatusCodes.Status404NotFound, res);
                }
            }
            else
            {
                return StatusCode(StatusCodes.Status401Unauthorized, res);
            }
        }

        /// <summary>
        /// Actualiza en BBDD el tramo indicado poniendo el valor de "Pagado" a true
        /// </summary>
        /// <param name="tramo">Objeto a buscar para actualizar el campo "Pagado"</param>
        /// <returns>Si se ha podido actualziar, devolvemos "Tramo pagado con éxito", si no, devolvemos "No se ha podido pagar el tramo\n"</returns>
        [HttpPost]
        [Route("PagarTramo")]
        public async Task<ActionResult<string>> PagarTramoAsync(TramoDTO tramo)
        {
            if (Request.Headers.ContainsKey("Token") && _palabraClave.Existe(Request.Headers["Token"].ToString()))
            {
                try
                {
                    TramoServicio tramoServicio = new TramoServicio(_context);
                    await tramoServicio.PagarTramoAsync(tramo.Id);
                    return StatusCode(StatusCodes.Status201Created, "Tramo pagado con éxito");
                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "No se ha podido pagar el tramo\n" + ex.Message);
                }
            }
            else
            {
                return StatusCode(StatusCodes.Status401Unauthorized, "Usuario no autorizado");
            }
        }
    }
}
