using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NaureBack.Models;

namespace NaureBack.Services
{
    public class CitaServicio
    {
        private readonly NaureContext _context;
        public CitaServicio(NaureContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Listamos todas las citas de la entidad
        /// </summary>
        /// <returns>Lista con todas las citas de la BBDD</returns>
        public async Task<IList<Cita>> ListarTodasCitasAsync()
        {
            IList<Cita> citas = await _context.Cita
                .Include(a=> a.IdContratacionNavigation)
                .ToListAsync();

            return citas;
        }

        /// <summary>
        /// Buscamos de las citas de la BBDD la que tiene como clave el valor de "id"
        /// </summary>
        /// <param name="id">Identificador a buscar en la entidad</param>
        /// <returns>Una unica entidad cita</returns>
        public async Task<Cita?> ObtenerCitaPorIdAsync(int id)
        {
            Cita? cita = await _context.Cita
                .Include(a => a.IdContratacionNavigation)
                .Where(b => b.Id == id)
                .SingleOrDefaultAsync();

            return cita;
        }

        /// <summary>
        /// Listamos todas las citas relacionadas con la contratación "idContratacion"
        /// </summary>
        /// <param name="idContratacion">Identificador de la contratación para la que buscar las citas</param>
        /// <returns>Lista con todas las citas que pertenecen a la contratación "idContratacion"</returns>
        public async Task<IList<Cita>> ListarCitasContratacionAsync(int idContratacion)
        {
            IList<Cita> citas = await _context.Cita
                .Include(a => a.IdContratacionNavigation)
                .Where(b => b.IdContratacion == idContratacion)
                .ToListAsync();

            return citas;
        }
    }
}
