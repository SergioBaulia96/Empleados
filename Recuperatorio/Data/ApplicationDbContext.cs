using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Recuperatorio.Models;

namespace Recuperatorio.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Empleado> Empleados {get; set;}
    public DbSet<Localidad> Localidades { get; set; }
    public DbSet<Sector> Sectores { get; set; }
}
