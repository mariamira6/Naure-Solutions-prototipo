using System;
using System.Collections.Generic;

namespace NaureBack.Models;

public partial class Cita
{
    public int Id { get; set; }

    public DateOnly? Fecha { get; set; }

    public int IdContratacion { get; set; }

    //PROYECTO
    //Los campos tienen que ser nullables si no, los pide en el modelo cuando pasamos un objeto de tipo Cita al POST por ejemplo
    public virtual Contratacion? IdContratacionNavigation { get; set; }
}
