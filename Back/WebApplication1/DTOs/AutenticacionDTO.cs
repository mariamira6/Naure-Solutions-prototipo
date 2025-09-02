using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Buffers.Text;
using System.Text.Json.Serialization;
using NaureBack.Models;

namespace NaureBack.DTOs
{
    public class AutenticacionDTO
    {
        public string? Login { get; set; }

        public string? Password { get; set; }

        public bool? Correcto { get; set; }

        public bool? EsAdmin { get; set; }

        public string? PalabraClave { get; set; }

        public string? Mensaje { get; set; }

        public int? IdUsuario
        {
            get; set;
        }
    }
}
