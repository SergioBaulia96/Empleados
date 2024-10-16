using System.ComponentModel.DataAnnotations;

namespace Recuperatorio.Models;

public class Sector
{
    [Key]

    public int SectorID { get; set;}
    public string? NombreSector { get; set; }
    public bool Eliminado { get; set; }
    public virtual ICollection<Empleado> Empleados { get; set; }
}

public class VistaSector
{
    public int SectorID { get; set; }
    public string? NombreSector { get; set; }
    public List<VistaLocalidad> ListadoLocalidades { get; set; }
}