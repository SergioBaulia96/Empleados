using System.ComponentModel.DataAnnotations;

namespace Recuperatorio.Models;

public class Localidad
{
    [Key]
    public int LocalidadID { get; set;}
    public string? NombreLocalidad { get; set;}
    public bool Eliminado { get; set; }

    public virtual ICollection<Empleado> Empleados { get; set;}
}

public class VistaLocalidad
{
    public int LocalidadID { get; set; }
    public string? NombreLocalidad { get; set; }
    public List<VistaEmpleado> ListadoEmpleados { get; set; }
}