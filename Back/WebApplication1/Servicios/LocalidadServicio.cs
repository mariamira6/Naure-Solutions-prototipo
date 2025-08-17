using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NaureBack.Converters;
using NaureBack.DTOs;
using NaureBack.Models;

namespace NaureBack.Services
{
    public class LocalidadServicio
    {
        private readonly NaureContext _context;
        public LocalidadServicio(NaureContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Listamos todas las localidades de la entidad
        /// </summary>
        /// <returns>Lista de todas las localidades de la BBDD</returns>
        public async Task<IList<Localidad>> ListarTodasLocalidadesAsync()
        {
            IList<Localidad> localidades = await _context.Localidads
                .ToListAsync();

            return localidades;
        }

        /// <summary>
        /// Buscamos de las localidades de la BBDD la que tiene como clave el valor de "id"
        /// </summary>
        /// <param name="id">Identificador a buscar en la entidad</param>
        /// <returns>Una unica entidad localidad</returns>
        public async Task<Localidad?> ObtenerLocalidadPorIdAsync(int id)
        {
            Localidad? localidad = await _context.Localidads
                .Where(a => a.Id == id)
                .SingleOrDefaultAsync();

            return localidad;
        }

        /// <summary>
        /// Buscamos todas las localidades que tienen una contratación. La relación es através de los clientes que son los que hacen contrataciones y 
        /// tienen una localidad asociada. Por cada contratación vendrá una localidad por lo que pueden venir repetidas dando constancia de la densidad
        /// de contrataciones en cada zona
        /// </summary>
        /// <returns>Lista con localidades que pueden venir repetidas</returns>
        public async Task<IList<LocalidadDTO>?> ObtenerLocalidadesConContratacionAsync()
        {
            IList<LocalidadDTO>? res = new List<LocalidadDTO>();
            ContratacionServicio contratacionServicio = new ContratacionServicio(_context);
            IList<Contratacion> contrataciones = await contratacionServicio.ListarTodasContratacionesAsync();
            LocalidadServicio localidadServicio = new LocalidadServicio(_context);
            IList<Localidad> localidades = await localidadServicio.ListarTodasLocalidadesAsync();
            foreach(Contratacion c in contrataciones)
            {
                Localidad aux = localidades.Single(i => i.Id == c.IdClienteNavigation.IdLocalidad);
                LocalidadConversor localidadConversor = new LocalidadConversor(aux);

                LocalidadDTO? localidadContratacionDTO = localidadConversor.LocalidadDTO;

                res.Add(localidadContratacionDTO);
            }

            return res;
        }
    }
}
