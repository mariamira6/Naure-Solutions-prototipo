using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NaureBack.DTOs;
using NaureBack.Models;

namespace NaureBack.Services
{
    public class ContratacionServicio
    {
        private readonly NaureContext _context;
        public ContratacionServicio(NaureContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Listamos todas las contrataciones de la entidad
        /// </summary>
        /// <returns>Lista con todas las contrataciones de la BBDD</returns>
        public async Task<IList<Contratacion>> ListarTodasContratacionesAsync()
        {
            IList<Contratacion> contrataciones = await _context.Contratacions
                .Include(a => a.IdServicioNavigation)
                .Include(b => b.IdClienteNavigation)
                .ToListAsync();

            return contrataciones;
        }

        /// <summary>
        /// Buscamos de las contrataciones de la BBDD que tengan como cliente a "idUsuario"
        /// </summary>
        /// <param name="idUsuario">Identificador del cliente para el que buscar las contrataciones</param>
        /// <returns>Lista con todas las contrataciones que pertenecen al cliente "idUsuario"</returns>
        public async Task<IList<Contratacion>> ListarContratacionesClienteAsync(int idUsuario)
        {
            IList<Contratacion> contrataciones = await _context.Contratacions
                .Include(a => a.IdServicioNavigation)
                .Include(b => b.IdClienteNavigation)
                .Where(c => c.IdCliente == idUsuario)
                .ToListAsync();

            return contrataciones;
        }

        /// <summary>
        /// Buscamos de las contrataciones de la BBDD la que tiene como clave el valor de "id"
        /// </summary>
        /// <param name="id">Identificador a buscar en la entidad</param>
        /// <returns>Una unica entidad contratación</returns>
        public async Task<Contratacion?> ObtenerContratacionPorIdAsync(int id)
        {
            Contratacion? contratacion = await _context.Contratacions
                .Include(a => a.IdServicioNavigation)
                .Include(b => b.IdClienteNavigation)
                .Where(c => c.Id == id)
                .SingleOrDefaultAsync();

            return contratacion;
        }
    }
}
