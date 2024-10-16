using System.ComponentModel.DataAnnotations;

namespace Recuperatorio.Models;

public class Empleado
{
    [Key]
    public int EmpleadoID { get; set;}
    public int LocalidadID { get; set;}
    public int SectorID { get; set; }
    public string? Nombre {get; set;}
    public string? Apellido {get; set;}
    public string? Direccion {get; set;}
    public DateTime Nacimiento {get; set;}
    public string? Telefono {get; set;}
    public string? Email {get; set;}
    public string? Salario {get; set;}
    public bool Eliminado { get; set;}
    public virtual Localidad Localidades { get; set; }
    public virtual Sector Sectores {get; set; }
}


public class VistaEmpleado
{
    public int EmpleadoID { get; set; }
    public int LocalidadID { get; set;}
    public int SectorID { get; set; }
    public string? Nombre {get; set;}
    public string? Apellido {get; set;}
    public string? Direccion { get; set;}
    public string? Nacimiento { get; set;}
    public string? Telefono { get; set;}
    public string? Email {get; set;}
    public string? Salario { get; set;}
    public string? NombreLocalidad { get; set; }
    public string? NombreSector { get; set; }
    public bool Eliminado { get; set; }
}

