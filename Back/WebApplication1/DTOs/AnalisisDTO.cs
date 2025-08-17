using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace NaureBack.DTOs
{
    public class AnalisisDTO
    {
        public int Anyo { get; set; }
        public int ServicioId { get; set; }
        public string? ServicioNombre { get; set; }
        public decimal Importe { get; set; }
        public decimal ImportePendiente { get; set; }
        public decimal ImporteCobrado { get; set; }
    }
}
