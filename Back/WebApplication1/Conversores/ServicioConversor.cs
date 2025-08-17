using NaureBack.DTOs;
using NaureBack.Models;

namespace NaureBack.Converters
{
    public class ServicioConversor
    {
        public ServicioDTO? ServicioDTO { get; }
        public IList<ServicioDTO>? ListaServiciosDTO { get; }
        public Servicio? Servicio { get; }
        
        public ServicioConversor(Servicio servicio)
        {
            ServicioDTO = ServicioToServicioDTO(servicio);
        }
        public ServicioConversor(IList<Servicio> servicios)
        {
            ListaServiciosDTO = new List<ServicioDTO>();

            foreach (Servicio s in servicios)
            {
                ListaServiciosDTO.Add(ServicioToServicioDTO(s));
            }
        }

        public ServicioConversor(ServicioDTO servicioDTO)
        {
            Servicio = ServicioDTOToServicio(servicioDTO);
        }

        private ServicioDTO ServicioToServicioDTO(Servicio servicio)
        {
            ServicioDTO res = new ServicioDTO()
            {
                Descripcion = servicio.Descripcion,
                Imagen = servicio.Imagen,
                Nombre = servicio.Nombre,
                Id = servicio.Id
            };

            return res;
        }

        private Servicio ServicioDTOToServicio(ServicioDTO servicioDTO)
        {
            Servicio res = new Servicio()
            {
                Descripcion = servicioDTO.Descripcion,
                Imagen = servicioDTO.Imagen,
                Nombre = servicioDTO.Nombre,
                Id = servicioDTO.Id
            };

            return res;
        }
    }
}
