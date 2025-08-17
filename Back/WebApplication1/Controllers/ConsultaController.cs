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
    public class ConsultaController : ControllerBase
    {
        private readonly NaureContext _context;

        public ConsultaController(NaureContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("InsertarConsulta")]
        public async Task<ActionResult<string>> InsertarConsultaAsync(ConsultaDTO consultaDTO)
        {
            try
            {
                ConsultaServicio consultaServicio = new ConsultaServicio(_context);
                await consultaServicio.InsertarConsultaAsync(consultaDTO);
                return StatusCode(StatusCodes.Status201Created, "Consulta creada con éxito");
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "No se ha podido insertar la consulta\n" + ex.Message);
            }
        }
    }
}
