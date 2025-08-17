using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace NaureBack.DTOs
{
    public class CitaDTO
    {
        public int Id { get; set; }
        public DateOnly? Fecha { get; set; }
        public int IdContratacion { get; set; }
        public ContratacionDTO? Contratacion { get; set; }
    }
}
