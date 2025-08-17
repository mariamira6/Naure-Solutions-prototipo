using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Buffers.Text;
using System.Text.Json.Serialization;
using NaureBack.Models;

namespace NaureBack.DTOs
{
    public class DocumentoDTO
    {
        public int Id { get; set; }

        public string? Nombre { get; set; }

        public string? Archivo { get; set; }

        public int IdServicio { get; set; }

        public ServicioDTO? Servicio { get; set; }
    }
}
