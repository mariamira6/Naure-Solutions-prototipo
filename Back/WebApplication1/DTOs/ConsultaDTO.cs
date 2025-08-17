using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NaureBack.Models;

namespace NaureBack.DTOs
{
    public class ConsultaDTO
    {
        public int Id { get; set; }

        public DateOnly? Fecha { get; set; }

        public string? Descripcion { get; set; }

        public string? Nombre { get; set; }

        public string? Apellidos { get; set; }

        public string? Empresa { get; set; }

        public string? Email { get; set; }

        public string? Telefono { get; set; }

        public bool? Revisada { get; set; }

        public int? IdServicio { get; set; }

        public int? IdCliente { get; set; }
    }
}
