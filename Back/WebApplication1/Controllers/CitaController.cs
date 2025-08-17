using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting.Internal;
using System.Net;
using NaureBack.Converters;
using NaureBack.DTOs;
using NaureBack.Models;
using NaureBack.Services;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace NaureBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitaController : ControllerBase
    {
        private readonly NaureContext _context;
        private readonly PalabraClaveServicio _palabraClave;

        public CitaController(NaureContext context, PalabraClaveServicio palabraClave)
        {
            _context = context;
            _palabraClave = palabraClave;
        }

        [HttpGet("ListarCitas")]
        public async Task<ActionResult<IList<CitaDTO>>> ListarCitasAsync()
        {
            IList<CitaDTO>? res = null;

            if (Request.Headers.ContainsKey("Token") && _palabraClave.Existe(Request.Headers["Token"].ToString()))
            {
                CitaServicio citaServicio = new CitaServicio(_context);
                IList<Cita> citas = await citaServicio.ListarTodasCitasAsync();
                CitaConversor conversor;

                if (citas != null)
                {
                    conversor = new CitaConversor(citas);
                    res = conversor.ListaCitaDTO;
                }

                return StatusCode(StatusCodes.Status200OK, res);
            }
            else
            {
                return StatusCode(StatusCodes.Status401Unauthorized, res);
            }
        }

        [HttpGet("ListarCitasContratacion")]
        public async Task<ActionResult<IList<CitaDTO>>> ListarCitasContratacionAsync(int idContratacion)
        {
            IList<CitaDTO>? res = null;

            if (Request.Headers.ContainsKey("Token") && _palabraClave.Existe(Request.Headers["Token"].ToString()))
            {
                CitaServicio citaServicio = new CitaServicio(_context);
                IList<Cita> citas = await citaServicio.ListarCitasContratacionAsync(idContratacion);
                CitaConversor conversor;

                if (citas != null)
                {
                    conversor = new CitaConversor(citas);
                    res = conversor.ListaCitaDTO;
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
