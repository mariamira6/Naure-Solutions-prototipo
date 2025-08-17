using NaureBack.DTOs;
using NaureBack.Models;

namespace NaureBack.Converters
{
    public class ContratacionConversor
    {
        public ContratacionDTO? ContratacionDTO { get; }
        public IList<ContratacionDTO>? ListaContratacionDTO { get; }
        
        public ContratacionConversor(Contratacion contratacion)
        {
            ContratacionDTO = ContratacionToContratacionDTO(contratacion);
        }
        public ContratacionConversor(IList<Contratacion> contrataciones)
        {
            ListaContratacionDTO = new List<ContratacionDTO>();

            foreach (Contratacion c in contrataciones)
            {
                ListaContratacionDTO.Add(ContratacionToContratacionDTO(c));
            }
        }

        private ContratacionDTO ContratacionToContratacionDTO(Contratacion contratacion)
        {
            ContratacionDTO res = new ContratacionDTO()
            {
                Id = contratacion.Id,
                IdCliente = contratacion.IdCliente,
                IdServicio = contratacion.IdServicio,
                FechaDesde = contratacion.FechaDesde,
                FechaHasta = contratacion.FechaHasta
            };

            if (contratacion.IdServicioNavigation != null)
            {
                ServicioConversor servicioConversor = new ServicioConversor(contratacion.IdServicioNavigation);
                res.Servicio = servicioConversor.ServicioDTO;
            }

            if (contratacion.IdClienteNavigation != null)
            {
                UsuarioConversor usuarioConversor = new UsuarioConversor(contratacion.IdClienteNavigation);
                res.Usuario = usuarioConversor.UsuarioDTO;
            }

            return res;
        }
    }
}
