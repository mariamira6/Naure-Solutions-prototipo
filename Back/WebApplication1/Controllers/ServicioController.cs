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
    public class ServicioController : ControllerBase
    {
        private readonly NaureContext _context;
        private readonly PalabraClaveServicio _palabraClave;

        public ServicioController(NaureContext context, PalabraClaveServicio palabraClave)
        {
            _context = context;
            _palabraClave = palabraClave;
        }

        /// <summary>
        /// Listar todos los servicios de la BBDD
        /// </summary>
        /// <returns>Lista con los servicios de la BBDD</returns>
        [HttpGet("ListarServicios")]
        public async Task<ActionResult<IList<ServicioDTO>>> ListarServiciosAsync()
        {
            ServicioServicio servicioServicio = new ServicioServicio(_context);
            IList<Servicio> servicios = await servicioServicio.ListarTodosServiciosAsync();
            IList<ServicioDTO>? res = null;
            ServicioConversor conversor;

            if (servicios != null)
            {
                conversor = new ServicioConversor(servicios);
                res = conversor.ListaServiciosDTO;
            }

            return StatusCode(StatusCodes.Status200OK, res); ;
        }

        /// <summary>
        /// Buscar servicio de la BBDD que tiene como clave el valor de "id"
        /// </summary>
        /// <param name="id">Identificador a buscar en la entidad</param>
        /// <returns>Una única entidad servicio o null en caso de no encontrarlo</returns>
        [HttpGet("ObtenerServicioPorId")]
        public async Task<ActionResult<ServicioDTO?>> ObtenerServicioPorIdAsync(int id)
        {
            ServicioServicio servicioServicio = new ServicioServicio(_context);
            Servicio? servicio = await servicioServicio.ObtenerServicioPorIdAsync(id);
            ServicioDTO? res = null;
            ServicioConversor conversor;

            if (servicio != null)
            {
                conversor = new ServicioConversor(servicio);
                res = conversor.ServicioDTO;
                return StatusCode(StatusCodes.Status200OK, res);
            }
            else
            {
                return StatusCode(StatusCodes.Status404NotFound, res);
            }
        }

        /// <summary>
        /// Introducimos en la BBDD un nuevo servicio
        /// </summary>
        /// <param name="servicioDTO">Nuevo servicio a introducir</param>
        /// <returns>Si se ha podido insertar, devolvemos "Servicio creado con éxito", si no, devolvemos "No se ha podido insertar el servicio\n"</returns>
        [HttpPost]
        [Route("InsertarServicio")]
        public async Task<ActionResult<string>> InsertarServicioAsync(ServicioDTO servicioDTO)
        {
            if (Request.Headers.ContainsKey("Token") && _palabraClave.Existe(Request.Headers["Token"].ToString()))
            {
                try
                {
                    ServicioServicio servicioServicio = new ServicioServicio(_context);
                    await servicioServicio.InsertarServicioAsync(servicioDTO);
                    return StatusCode(StatusCodes.Status201Created, "Servicio creado con éxito");
                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "No se ha podido insertar el servicio\n" + ex.Message);
                }
            }
            else
            {
                return StatusCode(StatusCodes.Status401Unauthorized, "Usuario no autorizado");
            }
        }

        /// <summary>
        /// Modificamos un servicio en la BBDD
        /// </summary>
        /// <param name="servicioDTO">Servicio a modificar</param>
        /// <returns>Si se ha podido modificar, devolvemos "Servicio modificado con éxito", si no, devolvemos "No se ha podido modificar el servicio\n"</returns>
        [HttpPost]
        [Route("ModificarServicio")]
        public async Task<ActionResult<string>> ModificarServicioAsync(ServicioDTO servicioDTO)
        {
            if (Request.Headers.ContainsKey("Token") && _palabraClave.Existe(Request.Headers["Token"].ToString()))
            {
                try
                {
                    ServicioServicio servicioServicio = new ServicioServicio(_context);
                    Servicio servicioRespuesta = await servicioServicio.ModificarServicioAsync(servicioDTO);
                    if (servicioRespuesta != null)
                    {
                        return StatusCode(StatusCodes.Status201Created, "Servicio modificado con éxito");
                    }
                    else
                    {
                        return StatusCode(StatusCodes.Status400BadRequest, "No se ha podido modificar el servicio\n");
                    }
                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "No se ha podido modificar el servicio\n" + ex.Message);
                }
            }
            else
            {
                return StatusCode(StatusCodes.Status401Unauthorized, "Usuario no autorizado");
            }
        }
    }
}
