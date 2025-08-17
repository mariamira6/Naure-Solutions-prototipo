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
    public class LocalidadController : ControllerBase
    {
        private readonly NaureContext _context;
        private readonly PalabraClaveServicio _palabraClave;

        public LocalidadController(NaureContext context, PalabraClaveServicio palabraClave)
        {
            _context = context;
            _palabraClave = palabraClave;
        }

        [HttpGet("ListarLocalidades")]
        public async Task<ActionResult<IList<LocalidadDTO>>> ListarLocalidadesAsync()
        {
            LocalidadServicio localidadServicio = new LocalidadServicio(_context);
            IList<Localidad> localidades = await localidadServicio.ListarTodasLocalidadesAsync();
            IList<LocalidadDTO>? res = null;
            LocalidadConversor conversor;

            if (localidades != null)
            {
                conversor = new LocalidadConversor(localidades);
                res = conversor.ListaLocalidadDTO;
            }

            return StatusCode(StatusCodes.Status200OK, res); ;
        }

        [HttpGet("ObtenerLocalidadPorId")]
        public async Task<ActionResult<LocalidadDTO?>> ObtenerLocalidadPorIdAsync(int id)
        {
            LocalidadServicio localidadServicio = new LocalidadServicio(_context);
            Localidad? localidad = await localidadServicio.ObtenerLocalidadPorIdAsync(id);
            LocalidadDTO? res = null;
            LocalidadConversor conversor;

            if (localidad != null)
            {
                conversor = new LocalidadConversor(localidad);
                res = conversor.LocalidadDTO;
                return StatusCode(StatusCodes.Status200OK, res);
            }
            else
            {
                return StatusCode(StatusCodes.Status404NotFound, res);
            }
        }
                
        [HttpGet("ObtenerLocalidadesConContratacion")]
        public async Task<ActionResult<IList<AnalisisDTO>>> ObtenerLocalidadesConContratacionAsync()
        {
            IList<LocalidadDTO>? datos = null;

            if (Request.Headers.ContainsKey("Token") && _palabraClave.Existe(Request.Headers["Token"].ToString()))
            {
                LocalidadServicio localidadServicio = new LocalidadServicio(_context);
                datos = await localidadServicio.ObtenerLocalidadesConContratacionAsync();

                return StatusCode(StatusCodes.Status200OK, datos);
            }
            else
            {
                return StatusCode(StatusCodes.Status401Unauthorized, datos);
            }
        }
    }
}
