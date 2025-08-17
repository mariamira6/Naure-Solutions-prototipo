using NaureBack.DTOs;
using NaureBack.Models;

namespace NaureBack.Converters
{
    public class DocumentoConversor
    {
        public DocumentoDTO? DocumentoDTO { get; }
        public IList<DocumentoDTO>? ListaDocumentoDTO { get; }

        public DocumentoConversor(Documento documento)
        {
            DocumentoDTO = DocumentoTDocumentoDTO(documento);
        }
        public DocumentoConversor(IList<Documento> documentos)
        {
            ListaDocumentoDTO = new List<DocumentoDTO>();

            foreach (Documento d in documentos)
            {
                ListaDocumentoDTO.Add(DocumentoTDocumentoDTO(d));
            }
        }

        private DocumentoDTO DocumentoTDocumentoDTO(Documento documento)
        {
            DocumentoDTO res = new DocumentoDTO()
            {
                Id = documento.Id,
                Archivo = documento.Archivo,
                IdServicio = documento.IdServicio,
                Nombre = documento.Nombre
            };

            if (documento.IdServicioNavigation != null)
            {
                ServicioConversor servicioConversor = new ServicioConversor(documento.IdServicioNavigation);
                res.Servicio = servicioConversor.ServicioDTO;
            }

            return res;
        }
    }
}
