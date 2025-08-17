using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NaureBack.Models;

namespace NaureBack.DTOs
{
    public class TramoDTO
    {
        public int Id { get; set; }

        public string? Descripcion { get; set; }

        public DateOnly? FechaDesde { get; set; }

        public DateOnly? FechaHasta { get; set; }

        public decimal? Importe { get; set; }

        public bool? Pagado { get; set; }

        public int IdContratacion { get; set; }

        public ContratacionDTO? Contratacion { get; set; } = null!;
    }
}
