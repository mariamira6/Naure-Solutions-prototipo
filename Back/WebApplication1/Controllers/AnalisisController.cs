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
    public class AnalisisController : ControllerBase
    {
        private readonly NaureContext _context;
        private readonly PalabraClaveServicio _palabraClave;

        public AnalisisController(NaureContext context, PalabraClaveServicio palabraClave)
        {
            _context = context;
            _palabraClave = palabraClave;
        }

        [HttpGet("ObtenerVentasPorAnyo")]
        public async Task<ActionResult<IList<AnalisisDTO>>> ObtenerVentasPorAnyoAsync()
        {
            IList<AnalisisDTO>? datos = null;

            if (Request.Headers.ContainsKey("Token") && _palabraClave.Existe(Request.Headers["Token"].ToString()))
            {
                AnalisisServicio analisisServicio = new AnalisisServicio(_context);
                datos = await analisisServicio.ObtenerVentasPorAnyoAsync();

                return StatusCode(StatusCodes.Status200OK, datos);
            }
            else
            {
                return StatusCode(StatusCodes.Status401Unauthorized, datos);
            }
        }

        ////[HttpGet("ObtenerVentasPorServicio")]
        ////public async Task<ActionResult<IList<AnalisisDTO>>> ObtenerVentasPorServicioAsync()
        ////{
        ////    AnalisisServicio analisisServicio = new AnalisisServicio(_context);
        ////    IList<AnalisisDTO> datos = await analisisServicio.ObtenerVentasPorServicioAsync();

        ////    return StatusCode(StatusCodes.Status200OK, datos);
        ////}

        [HttpGet("ObtenerVentasPorServicio")]
        public async Task<ActionResult<IList<AnalisisDTO>>> ObtenerVentasPorServicioAsync()
        {
            IList<AnalisisDTO>? datos = null;

            if (Request.Headers.ContainsKey("Token") && _palabraClave.Existe(Request.Headers["Token"].ToString()))
            {
                AnalisisServicio analisisServicio = new AnalisisServicio(_context);
                datos = await analisisServicio.ObtenerVentasPorServicioAsync();

                return StatusCode(StatusCodes.Status200OK, datos);
            }
            else
            {
                return StatusCode(StatusCodes.Status401Unauthorized, datos);
            }
        }
    }
}
