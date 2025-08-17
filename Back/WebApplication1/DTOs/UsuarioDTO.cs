using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Buffers.Text;
using System.Text.Json.Serialization;
using NaureBack.Models;

namespace NaureBack.DTOs
{
    public class UsuarioDTO
    {
        public int Id { get; set; }

        public string? Nombre { get; set; }

        public string? Apellidos { get; set; }

        public string? Login { get; set; }

        public string? Password { get; set; }

        public bool? EsAdmin { get; set; }

        public string? Empresa { get; set; }

        public string? Email { get; set; }

        public string? Direccion { get; set; }

        public string? Telefono { get; set; }

        public string? CuentaBancaria { get; set; }

        public int IdLocalidad { get; set; }

        public LocalidadDTO? Localidad { get; set; }
    }
}
