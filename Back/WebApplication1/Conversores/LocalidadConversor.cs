using NaureBack.DTOs;
using NaureBack.Models;

namespace NaureBack.Converters
{
    public class LocalidadConversor
    {
        public LocalidadDTO? LocalidadDTO { get; }
        public IList<LocalidadDTO>? ListaLocalidadDTO { get; }
        
        public LocalidadConversor(Localidad localidad)
        {
            LocalidadDTO = LocalidadToLocalidadDTO(localidad);
        }
        public LocalidadConversor(IList<Localidad> localidades)
        {
            ListaLocalidadDTO = new List<LocalidadDTO>();

            foreach (Localidad l in localidades)
            {
                ListaLocalidadDTO.Add(LocalidadToLocalidadDTO(l));
            }
        }

        private LocalidadDTO LocalidadToLocalidadDTO(Localidad localidad)
        {
            LocalidadDTO res = new LocalidadDTO()
            {
                Id = localidad.Id,
                Geometria = localidad.Geometria,
                Nombre = localidad.Nombre
            };

            return res;
        }
    }
}
