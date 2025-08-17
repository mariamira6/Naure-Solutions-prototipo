using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NaureBack.DTOs;
using NaureBack.Models;

namespace NaureBack.Services
{
    public class TramoServicio
    {
        private readonly NaureContext _context;
        public TramoServicio(NaureContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Listamos todos los tramos de la entidad
        /// Se incluyen los datos de la contratación asociada a cada tramo
        /// </summary>
        /// <returns>Lista de todos los tramos de la BBDD</returns>
        public async Task<IList<Tramo>> ListarTodosTramosAsync()
        {
            IList<Tramo> tramos = await _context.Tramos
                .Include(a=> a.IdContratacionNavigation)
                .ToListAsync();

            return tramos;
        }

        /// <summary>
        /// Listamos todos los tramos relacionados con la contratación "idContratacion"
        /// Se incluyen los datos de la contratación asociada a cada tramo
        /// </summary>
        /// <param name="idContratacion">Identificador de la contratación para buscar los tramos</param>
        /// <returns>Lista de tramos de la contratación</returns>
        public async Task<IList<Tramo>> ListarTramosContratacionAsync(int idContratacion)
        {
            IList<Tramo> tramos = await _context.Tramos
                .Include(a => a.IdContratacionNavigation)
                .Where(b => b.IdContratacion == idContratacion)
                .ToListAsync();

            return tramos;
        }

        /// <summary>
        /// Buscamos de los tramos de la BBDD el que tiene como clave el valor de "id"
        /// </summary>
        /// <param name="id">Identificador a buscar en la entidad</param>
        /// <returns>Una unica entidad tramo</returns>
        public async Task<Tramo?> ObtenerTramoPorIdAsync(int id)
        {
            Tramo? tramo = await _context.Tramos
                .Include(a => a.IdContratacionNavigation)
                .Where(b => b.Id == id)
                .SingleOrDefaultAsync();

            return tramo;
        }

        /// <summary>
        /// /// Actualiza en BBDD el tramo indicado poniendo el valor de "Pagado" a true
        /// </summary>
        /// <param name="idTramo">Identificador a buscar para actualizar el campo "Pagado"</param>
        /// <returns></returns>
        /// <exception cref="Exception">Si hay algún problema en el guardado del tramo en la BBDD lanza una excepción</exception>
        public async Task PagarTramoAsync(int idTramo)
        {
            try
            {
                Tramo? tramo = await ObtenerTramoPorIdAsync(idTramo);
                tramo.Pagado = true;
                _context.Tramos.Update(tramo);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
