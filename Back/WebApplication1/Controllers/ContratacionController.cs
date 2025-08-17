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
    public class ContratacionController : ControllerBase
    {
        private readonly NaureContext _context;
        private readonly PalabraClaveServicio _palabraClave;

        public ContratacionController(NaureContext context, PalabraClaveServicio palabraClave)
        {
            _context = context;
            _palabraClave = palabraClave;
        }

        [HttpGet("ListarContrataciones")]
        public async Task<ActionResult<IList<ContratacionDTO>>> ListarContratacionesAsync()
        {
            IList<ContratacionDTO>? res = null;

            if (Request.Headers.ContainsKey("Token") && _palabraClave.Existe(Request.Headers["Token"].ToString()))
            {
                ContratacionServicio contratacionServicio = new ContratacionServicio(_context);
                IList<Contratacion> contratacion = await contratacionServicio.ListarTodasContratacionesAsync();
                ContratacionConversor conversor;

                if (contratacion != null)
                {
                    conversor = new ContratacionConversor(contratacion);
                    res = conversor.ListaContratacionDTO;
                }

                return StatusCode(StatusCodes.Status200OK, res);
            }
            else
            {
                return StatusCode(StatusCodes.Status401Unauthorized, res);
            }
        }

        [HttpGet("ObtenerContratacionPorId")]
        public async Task<ActionResult<ContratacionDTO?>> ObtenerContratacionPorIdAsync(int id)
        {
            ContratacionDTO? res = null;

            if (Request.Headers.ContainsKey("Token") && _palabraClave.Existe(Request.Headers["Token"].ToString()))
            {
                ContratacionServicio contratacionServicio = new ContratacionServicio(_context);
                Contratacion? contratacion = await contratacionServicio.ObtenerContratacionPorIdAsync(id);
                ContratacionConversor conversor;

                if (contratacion != null)
                {
                    conversor = new ContratacionConversor(contratacion);
                    res = conversor.ContratacionDTO;
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
    }
}
