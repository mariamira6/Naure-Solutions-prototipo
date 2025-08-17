using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Buffers.Text;
using System.Text.Json.Serialization;
using NaureBack.Models;

namespace NaureBack.DTOs
{
    public class LocalidadDTO
    {
        public int Id { get; set; }

        public string? Nombre { get; set; }

        public string? Geometria { get; set; }
    }
}
