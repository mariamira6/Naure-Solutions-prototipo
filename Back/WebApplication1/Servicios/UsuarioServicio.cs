using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NaureBack.DTOs;
using NaureBack.Models;

namespace NaureBack.Services
{
    public class UsuarioServicio
    {
        private readonly NaureContext _context;
        public UsuarioServicio(NaureContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Listamos todos los usuarios de la entidad
        /// </summary>
        /// <returns>Lista de todos los usuarios de la BBDD</returns>
        public async Task<IList<Usuario>> ListarTodosUsuariosAsync()
        {
            IList<Usuario> usuarios = await _context.Usuarios
                .ToListAsync();

            return usuarios;
        }

        /// <summary>
        /// Buscamos de los usuarios de la BBDD el que tiene como clave el valor de "id"
        /// </summary>
        /// <param name="id">Identificador a buscar en la entidad</param>
        /// <returns>Una unica entidad usuario</returns>
        public async Task<Usuario?> ObtenerUsuarioPorIdAsync(int id)
        {
            Usuario? usuario = await _context.Usuarios
                .Where(a => a.Id == id)
                .SingleOrDefaultAsync();

            return usuario;
        }

        /// <summary>
        /// Buscamos en la entidad un usuario que tenga el "Login" y "Password" que mandan en el objeto "autenticacionDTO"
        /// </summary>
        /// <param name="autenticacionDTO">Objeto con datos de autenticación para buscar en la BBDD</param>
        /// <returns>Si en contramos el usuario que coincida con las credenciales, devolvemos la entidad Usuario, de lo contrario, devolvemos null</returns>
        public async Task<Usuario?> ComprobarCredencialesAsync(AutenticacionDTO autenticacionDTO)
        {
            Usuario? usuario = await _context.Usuarios
                .Where(a => a.Login == autenticacionDTO.Login && a.Password == autenticacionDTO.Password)
                .SingleOrDefaultAsync();

            return usuario;
        }
    }
}
