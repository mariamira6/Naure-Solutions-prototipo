using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NaureBack.DTOs;
using NaureBack.Models;

namespace NaureBack.Services
{
    public class DocumentoServicio
    {
        private readonly NaureContext _context;
        public DocumentoServicio(NaureContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Listamos todos los documentos relacionados con el servicio "idServicio"
        /// Se adjunta el archivo físico
        /// </summary>
        /// <param name="idServicio">Identificador del servicio para buscar los documentos</param>
        /// <returns>Lista de documentos del servicio</returns>
        public async Task<IList<Documento>> ListarDocumentoServicioAsync(int idServicio)
        {
            IList<Documento> documentos = await _context.Documentos
                .Include(a => a.IdServicioNavigation)
                .Where(b => b.IdServicio == idServicio)
                .ToListAsync();

            return documentos;
        }

        /// <summary>
        /// Listamos todos los documentos relacionados con el servicio "idServicio"
        /// No se adjunta el archivo físico
        /// </summary>
        /// <param name="idServicio">Identificador del servicio para buscar los documentos</param>
        /// <returns>Lista de documentos del servicio pero sin el archivo físico</returns>
        public async Task<IList<DocumentoDTO>> ListarDocumentoServicioLiteAsync(int idServicio)
        {
            IList<DocumentoDTO> res = new List<DocumentoDTO>();

            var documentos = await _context.Documentos
                .Include(a => a.IdServicioNavigation)
                .Where(b => b.IdServicio == idServicio)
                .Select(c => new { c.Id, c.Nombre, c.IdServicio })
                .ToListAsync();

            foreach(var d in documentos)
            {
                DocumentoDTO documentoDTO = new DocumentoDTO()
                {
                    Id = d.Id,
                    Nombre = d.Nombre,
                    IdServicio = d.IdServicio
                };

                res.Add(documentoDTO);
            }

            return res;
        }

        /// <summary>
        /// Buscamos de los documentos de la BBDD el que tiene como clave el valor de "id"
        /// </summary>
        /// <param name="id">Identificador a buscar en la entidad</param>
        /// <returns>Una unica entidad documento</returns>
        public async Task<Documento?> ObtenerDocumentoPorIdAsync(int id)
        {
            Documento? documento = await _context.Documentos
                .Include(a => a.IdServicioNavigation)
                .Where(b => b.Id == id)
                .SingleOrDefaultAsync();

            return documento;
        }

        /// <summary>
        /// Actualiza en BBDD el documento pasado por parámetro
        /// </summary>
        /// <param name="documentoDTO">Objeto con los valores de documento para actualizar</param>
        /// <returns>No retorna nada</returns>
        /// <exception cref="Exception">Si hay algún problema en la inserción en la BBDD lanza una excepción</exception>
        public async Task ActualizarFicheroAsync(DocumentoDTO documentoDTO)
        {
            try
            {
                Documento? documento = await ObtenerDocumentoPorIdAsync(documentoDTO.Id);
                documento.Archivo = documentoDTO.Archivo;
                documento.Nombre = documentoDTO.Nombre;
                _context.Documentos.Update(documento);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
