using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Buffers.Text;
using System.Text.Json.Serialization;
using NaureBack.Models;

namespace NaureBack.DTOs
{
    public class ContratacionDTO
    {
        public int Id { get; set; }

        public DateOnly? FechaDesde { get; set; }

        public DateOnly? FechaHasta { get; set; }

        public int IdCliente { get; set; }

        public int IdServicio { get; set; }

        public ServicioDTO? Servicio { get; set; }

        public UsuarioDTO? Usuario { get; set; }
    }
}
