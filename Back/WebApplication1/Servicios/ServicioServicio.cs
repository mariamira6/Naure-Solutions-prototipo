using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NaureBack.DTOs;
using NaureBack.Models;
using System.Reflection.Metadata.Ecma335;

namespace NaureBack.Services
{
    public class ServicioServicio
    {
        private readonly NaureContext _context;
        public ServicioServicio(NaureContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Listamos todos los servicios de la entidad incluyendo la imagen asociada
        /// </summary>
        /// <returns>Lista de todos los servicios de la BBDD</returns>
        public async Task<IList<Servicio>> ListarTodosServiciosAsync()
        {
            IList<Servicio> servicios = await _context.Servicios.ToListAsync();

            return servicios;
        }

        /// <summary>
        /// Buscamos de los servicios de la BBDD el que tiene como clave el valor de "id"
        /// </summary>
        /// <param name="id">Identificador a buscar en la entidad</param>
        /// <returns>Una única entidad servicio</returns>
        public async Task<Servicio?> ObtenerServicioPorIdAsync(int id)
        {
            Servicio? servicio = await  _context.Servicios
                .SingleOrDefaultAsync(a => a.Id == id);

            return servicio;
        }

        /// <summary>
        /// Introducimos en la BBDD un nuevo servicio
        /// </summary>
        /// <param name="servicioDTO">Nuevo servicio a introducir</param>
        /// <returns>No retorna nada</returns>
        /// <exception cref="Exception">Si hay algún problema en la inserción en la BBDD lanza una excepción</exception>
        public async Task InsertarServicioAsync(ServicioDTO servicioDTO)
        {
            try
            {
                Servicio nuevoServicio = new Servicio()
                {
                    Imagen = servicioDTO.Imagen,
                    Nombre = servicioDTO.Nombre,
                    Descripcion = servicioDTO.Descripcion
                };

                await _context.Servicios.AddAsync(nuevoServicio);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Guardamos los cambios en la BBDD un servicio
        /// </summary>
        /// <param name="servicioDTO">Servicio a modificar</param>
        /// <returns>No retorna nada</returns>
        /// <exception cref="Exception">Si hay algún problema en la inserción en la BBDD lanza una excepción</exception>
        public async Task<Servicio> ModificarServicioAsync(ServicioDTO servicioDTO)
        {
            Servicio? servicio = null;

            try
            {
                servicio = await ObtenerServicioPorIdAsync(servicioDTO.Id);

                if (servicio != null)
                {
                    servicio.Descripcion = servicioDTO.Descripcion;
                    servicio.Nombre = servicioDTO.Nombre;
                    servicio.Imagen = servicioDTO.Imagen;


                    _context.Servicios.Update(servicio);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return servicio;
        }
    }
}
