using NaureBack.DTOs;
using NaureBack.Models;

namespace NaureBack.Converters
{
    public class CitaConversor
    {
        public CitaDTO? CitaUnicaDTO { get; }
        public IList<CitaDTO>? ListaCitaDTO { get; }

        public CitaConversor(Cita cita)
        {
            CitaUnicaDTO = CitaToCitaDTO(cita);
        }
        public CitaConversor(IList<Cita> citas)
        {
            ListaCitaDTO = new List<CitaDTO>();

            foreach (Cita c in citas)
            {
                ListaCitaDTO.Add(CitaToCitaDTO(c));
            }
        }

        private CitaDTO CitaToCitaDTO(Cita cita)
        {
            CitaDTO res = new CitaDTO()
            {
                Id = cita.Id,
                Fecha = cita.Fecha,
                IdContratacion = cita.IdContratacion
            };

            if (cita.IdContratacionNavigation != null)
            {
                ContratacionConversor contratacionConversor = new ContratacionConversor(cita.IdContratacionNavigation);
                res.Contratacion = contratacionConversor.ContratacionDTO;
            }

            return res;
        }
    }
}
