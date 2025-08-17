using System;
using System.Collections.Generic;

namespace NaureBack.Models;

public partial class Usuario
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

    public virtual ICollection<Consulta> Consulta { get; set; } = new List<Consulta>();

    public virtual ICollection<Contratacion> Contratacions { get; set; } = new List<Contratacion>();

    public virtual Localidad IdLocalidadNavigation { get; set; } = null!;
}
