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
    public class UsuarioController : ControllerBase
    {
        private readonly NaureContext _context;
        private readonly PalabraClaveServicio _palabraClave;

        public UsuarioController(NaureContext context, PalabraClaveServicio palabraClave)
        {
            _context = context;
            _palabraClave = palabraClave;
        }

        [HttpGet("ObtenerUsuarios")]
        public async Task<ActionResult<IList<UsuarioDTO>>> ObtenerUsuariosAsync()
        {
            IList<UsuarioDTO>? res = null;

            if (Request.Headers.ContainsKey("Token") && _palabraClave.Existe(Request.Headers["Token"].ToString()))
            {
                UsuarioServicio usuarioServicio = new UsuarioServicio(_context);
                IList<Usuario> usuarios = await usuarioServicio.ListarTodosUsuariosAsync();
                UsuarioConversor conversor;

                if (usuarios != null)
                {
                    conversor = new UsuarioConversor(usuarios);
                    res = conversor.ListaUsuariosDTO;
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

        [HttpGet("ObtenerUsuarioPorId")]
        public async Task<ActionResult<UsuarioDTO>> ObtenerUsuarioPorIdAsync(int id)
        {
            UsuarioDTO? res = null;

            if (Request.Headers.ContainsKey("Token") && _palabraClave.Existe(Request.Headers["Token"].ToString()))
            {
                UsuarioServicio usuarioServicio = new UsuarioServicio(_context);
                Usuario? usuario = await usuarioServicio.ObtenerUsuarioPorIdAsync(id);
                UsuarioConversor conversor;

                if (usuario != null)
                {
                    conversor = new UsuarioConversor(usuario);
                    res = conversor.UsuarioDTO;
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
        /// Buscamos en la entidad un usuario que tenga el "Login" y "Password" que se mande en el objeto "autenticacionDTO"
        /// </summary>
        /// <param name="autenticacionDTO">Objeto con datos de autenticación para buscar en la BBDD</param>
        /// <returns>Devolvemos un objeto de tipo "AutenticacionDTO" con los datos de resultado de la validación del usuario</returns>
        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult<AutenticacionDTO>> LoginAsync(AutenticacionDTO autenticacionDTO)
        {
            AutenticacionDTO res = new AutenticacionDTO()
            {
                Login = autenticacionDTO.Login,
                Password = autenticacionDTO.Password
            };

            try
            {
                UsuarioServicio servicioUsuario = new UsuarioServicio(_context);
                Usuario? usuario = await servicioUsuario.ComprobarCredencialesAsync(autenticacionDTO);

                if (usuario != null)
                {
                    res.Correcto = true;
                    res.Mensaje = "Usuario autenticado con éxito";
                    res.PalabraClave = Guid.NewGuid().ToString();
                    res.EsAdmin = usuario.EsAdmin;

                    _palabraClave.Insertar(res.PalabraClave);
                }
                else
                {
                    throw new Exception();
                }

                return StatusCode(StatusCodes.Status200OK, res);
            }
            catch
            {
                res.Correcto = false;
                res.Mensaje = "Usuario y password incorrectos";
                return StatusCode(StatusCodes.Status401Unauthorized, res);
            }
        }
    }
}
