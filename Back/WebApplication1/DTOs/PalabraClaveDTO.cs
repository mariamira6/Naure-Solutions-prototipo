using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Buffers.Text;
using System.Text.Json.Serialization;
using NaureBack.Models;

namespace NaureBack.DTOs
{
    public class PalabraClaveDTO
    {
        public string? PalabraClave { get; set; }

        public DateTime? FechaTope { get; set; }
    }
}
