using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NaureBack.DTOs;
using NaureBack.Models;

namespace NaureBack.Services
{
    public class ConsultaServicio
    {
        private readonly NaureContext _context;
        public ConsultaServicio(NaureContext context)
        {
            _context = context;
        }
        
        /// <summary>
        /// Introducimos en la BBDD una nueva consulta
        /// </summary>
        /// <param name="consultaDTO">Estructura de datos con los datos de la consulta</param>
        /// <returns>No retorna nada</returns>
        /// <exception cref="Exception">Si hay algún problema en la inserción en la BBDD lanza una excepción</exception>
        public async Task InsertarConsultaAsync(ConsultaDTO consultaDTO)
        {
            try
            {
                //Creamos una entidad del modelo "Consulta" para poder meterlo en la BBDD mediante EntityFramework
                Consulta nuevaConsulta = new Consulta()
                {
                    Apellidos= consultaDTO.Apellidos,
                    Descripcion = consultaDTO.Descripcion,
                    Email = consultaDTO.Email,
                    Empresa = consultaDTO.Empresa,
                    Fecha = consultaDTO.Fecha,
                    IdCliente = consultaDTO.IdCliente,
                    IdServicio = consultaDTO.IdServicio,
                    Nombre = consultaDTO.Nombre,
                    Revisada = false,
                    Telefono = consultaDTO.Telefono                    
                };

                await _context.Consulta.AddAsync(nuevaConsulta);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
