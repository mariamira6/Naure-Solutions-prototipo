using System.Diagnostics;
using NaureBack.DTOs;
using NaureBack.Models;

namespace NaureBack.Converters
{
    public class TramoConversor
    {
        public TramoDTO? TramoUnicoDTO { get; }
        public IList<TramoDTO>? ListaTramoDTO { get; }

        public TramoConversor(Tramo tramo)
        {
            TramoUnicoDTO = TramoToTramoDTO(tramo);
        }
        public TramoConversor(IList<Tramo> tramos)
        {
            ListaTramoDTO = new List<TramoDTO>();

            foreach (Tramo t in tramos)
            {
                ListaTramoDTO.Add(TramoToTramoDTO(t));
            }
        }

        private TramoDTO TramoToTramoDTO(Tramo tramo)
        {
            TramoDTO res = new TramoDTO()
            {
                Id = tramo.Id,
                Descripcion= tramo.Descripcion,
                FechaDesde= tramo.FechaDesde,
                FechaHasta = tramo.FechaHasta,
                IdContratacion= tramo.IdContratacion,
                Importe = tramo.Importe,
                Pagado = tramo.Pagado
            };

            if (tramo.IdContratacionNavigation != null)
            {
                ContratacionConversor contratacionConversor = new ContratacionConversor(tramo.IdContratacionNavigation);
                res.Contratacion = contratacionConversor.ContratacionDTO;
            }

            return res;
        }
    }
}
