using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting.Internal;
using System.Net;
using NaureBack.Converters;
using NaureBack.DTOs;
using NaureBack.Models;
using NaureBack.Services;
using System.Reflection.Metadata;

namespace NaureBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentoController : ControllerBase
    {
        private readonly NaureContext _context;
        private readonly PalabraClaveServicio _palabraClave;

        public DocumentoController(NaureContext context, PalabraClaveServicio palabraClave)
        {
            _context = context;
            _palabraClave = palabraClave;
        }

        [HttpGet("ObtenerDocumentosPorServicio")]
        public async Task<ActionResult<IList<DocumentoDTO>>> ObtenerDocumentosPorServicioAsync(int idServicio)
        {
            IList<DocumentoDTO>? res = null;

            if (Request.Headers.ContainsKey("Token") && _palabraClave.Existe(Request.Headers["Token"].ToString()))
            {
                DocumentoServicio documentoServicio = new DocumentoServicio(_context);
                IList<Documento> documentos = await documentoServicio.ListarDocumentoServicioAsync(idServicio);
                DocumentoConversor conversor;

                if (documentos != null)
                {
                    conversor = new DocumentoConversor(documentos);
                    res = conversor.ListaDocumentoDTO;
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

        [HttpGet("ObtenerDocumentosSinArchivoPorServicio")]
        public async Task<ActionResult<IList<DocumentoDTO>>> ObtenerDocumentosSinArchivoPorServicioAsync(int idServicio)
        {
            IList<DocumentoDTO>? documentos = null;

            if (Request.Headers.ContainsKey("Token") && _palabraClave.Existe(Request.Headers["Token"].ToString()))
            {
                DocumentoServicio documentoServicio = new DocumentoServicio(_context);
                documentos = await documentoServicio.ListarDocumentoServicioLiteAsync(idServicio);

                if (documentos != null)
                {
                    return StatusCode(StatusCodes.Status200OK, documentos);
                }
                else
                {
                    return StatusCode(StatusCodes.Status404NotFound, documentos);
                }
            }
            else
            {
                return StatusCode(StatusCodes.Status401Unauthorized, documentos);
            }
        }

        [HttpGet("ObtenerDocumentoPorId")]
        public async Task<ActionResult<DocumentoDTO>> ObtenerDocumentoPorIdAsync(int id)
        {
            DocumentoDTO? res = null;

            if (Request.Headers.ContainsKey("Token") && _palabraClave.Existe(Request.Headers["Token"].ToString()))
            {
                DocumentoServicio documentoServicio = new DocumentoServicio(_context);
                Documento? documento = await documentoServicio.ObtenerDocumentoPorIdAsync(id);
                DocumentoConversor conversor;

                if (documento != null)
                {
                    conversor = new DocumentoConversor(documento);
                    res = conversor.DocumentoDTO;
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

        [HttpPost]
        [Route("SubirArchivo")]
        public async Task<ActionResult<string>> SubirArchivoAsync(DocumentoDTO documento)
        {
            if (Request.Headers.ContainsKey("Token") && _palabraClave.Existe(Request.Headers["Token"].ToString()))
            {
                try
                {
                    DocumentoServicio documentoServicio = new DocumentoServicio(_context);
                    await documentoServicio.ActualizarFicheroAsync(documento);
                    return StatusCode(StatusCodes.Status201Created, "Archivo actualizado con éxito");
                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "No se ha podido actualizar el archivo\n" + ex.Message);
                }
            }
            else
            {
                return StatusCode(StatusCodes.Status401Unauthorized, "Usuario no autorizado");
            }
        }
    }
}
