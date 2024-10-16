using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Recuperatorio.Data;
using Recuperatorio.Models;

namespace Recuperatorio.Controllers;


public class EmpleadosController : Controller
{
    private ApplicationDbContext _context;
    public EmpleadosController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Empleados()
    {
        var selectListItems = new List<SelectListItem>
        {
            new SelectListItem { Value = "0", Text = "[SELECCIONE...]"}
        };

        ViewBag.LocalidadID = selectListItems.OrderBy(t => t.Text).ToList();
        ViewBag.SectorID = selectListItems.OrderBy(t => t.Text).ToList();

        var localidades = _context.Localidades.ToList();
        var sectores = _context.Sectores.ToList();
        var localidadesBuscar = _context.Localidades.ToList();

        localidades.Add(new Localidad { LocalidadID = 0, NombreLocalidad = "[SELECCIONE...]" });
        ViewBag.LocalidadID = new SelectList(localidades.OrderBy(c => c.NombreLocalidad), "LocalidadID", "NombreLocalidad");

        sectores.Add(new Sector { SectorID = 0, NombreSector = "[SECTOR...]" });
        ViewBag.SectorID = new SelectList(sectores.OrderBy(c => c.NombreSector), "SectorID", "NombreSector");

        localidadesBuscar.Add(new Localidad { LocalidadID = 0, NombreLocalidad = "[SELECCIONE...]" });
        ViewBag.BuscarLocalidad = new SelectList(localidadesBuscar.OrderBy(c => c.NombreLocalidad), "LocalidadID", "NombreLocalidad");

        return View();
    }

    public JsonResult ListadoDeEmpleados(int? EmpleadoID)
    {
        List<VistaEmpleado> empleadosMostrar = new List<VistaEmpleado>();

        var empleados = _context.Empleados.ToList();
        empleados = empleados.OrderBy(e => e.Nombre).ToList();

        if (EmpleadoID != null)
        {
            empleados = empleados.Where(t => t.EmpleadoID == EmpleadoID).ToList();
        }

        var localidades = _context.Localidades.ToList();
        var sectores = _context.Sectores.ToList();

        foreach (var e in empleados)
        {
            var localidad = localidades.Where(t => t.LocalidadID == e.LocalidadID).SingleOrDefault();
            var sector = sectores.Where(t => t.SectorID == e.SectorID).SingleOrDefault();

            var empleadoMostrar = new VistaEmpleado
            {
                EmpleadoID = e.EmpleadoID,
                LocalidadID = e.LocalidadID,
                SectorID = e.SectorID,
                Nombre = e.Nombre,
                NombreLocalidad = localidad?.NombreLocalidad,
                NombreSector = sector?.NombreSector,
                Apellido = e.Apellido,
                Nacimiento = e.Nacimiento.ToString("dd/MM/yyyy"),
                Telefono = e.Telefono,
                Email = e.Email,
                Salario = e.Salario,
                Direccion = e.Direccion
            };
            empleadosMostrar.Add(empleadoMostrar);
        }

        return Json(empleadosMostrar);
    }

    public JsonResult TraerEmpleadosAlModal(int? EmpleadoID)
    {
        var empleadosPorID = _context.Empleados.ToList();

        if (EmpleadoID != null)
        {
            empleadosPorID = empleadosPorID.Where(t => t.EmpleadoID == EmpleadoID).ToList();
        }
        return Json(empleadosPorID.ToList());
    }

    public JsonResult GuardarEmpleado(int EmpleadoID, int LocalidadID, int SectorID, string Nombre, string Apellido, string Direccion, DateTime Nacimiento, string Telefono, string Email, string Salario)
    {
        string resultado = "";

        Nombre = Nombre.ToUpper();
        Apellido = Apellido.ToUpper();
        Direccion = Direccion.ToUpper();

        if (EmpleadoID == 0 && LocalidadID > 0 && SectorID > 0)
        {
            var nuevoEmpleado = new Empleado
            {
                EmpleadoID = EmpleadoID,
                LocalidadID = LocalidadID,
                SectorID = SectorID,
                Nombre = Nombre,
                Apellido = Apellido,
                Direccion = Direccion,
                Nacimiento = Nacimiento,
                Telefono = Telefono,
                Email = Email,
                Salario = Salario
            };
            _context.Add(nuevoEmpleado);
            _context.SaveChanges();
            resultado = "Empleado guardado correctamente";
        }
        else
        {
            var editarEmpleado = _context.Empleados.Where(e => e.EmpleadoID == EmpleadoID).SingleOrDefault();

            if (editarEmpleado != null)
            {
                editarEmpleado.LocalidadID = LocalidadID;
                editarEmpleado.SectorID = SectorID;
                editarEmpleado.Nombre = Nombre;
                editarEmpleado.Apellido = Apellido;
                editarEmpleado.Direccion = Direccion;
                editarEmpleado.Nacimiento = Nacimiento;
                editarEmpleado.Telefono = Telefono;
                editarEmpleado.Email = Email;
                editarEmpleado.Salario = Salario;
                _context.SaveChanges();
                resultado = "Empleado editado correctamente";
            }
        }
        return Json(resultado);
    }

    public JsonResult EliminarEmpleado(int EmpleadoID)
    {
        var eliminarEmpleado = _context.Empleados.Find(EmpleadoID);
        _context.Remove(eliminarEmpleado);
        _context.SaveChanges();

        return Json(eliminarEmpleado);
    }

    public IActionResult Index()
    {
        var selectListItems = new List<SelectListItem>
        {
            new SelectListItem { Value = "0", Text = "[SELECCIONE...]"}
        };
        ViewBag.LocalidadID = selectListItems.OrderBy(t => t.Text).ToList();


        var localidadesBuscar = _context.Localidades.ToList();

        localidadesBuscar.Add(new Localidad { LocalidadID = 0, NombreLocalidad = "[SELECCIONE...]" });
        ViewBag.BuscarLocalidad = new SelectList(localidadesBuscar.OrderBy(c => c.NombreLocalidad), "LocalidadID", "NombreLocalidad");

        return View();
    }

    public JsonResult ListadoInformes(int? buscarLocalidad)
    {
        List<VistaLocalidad> localidadesMostrar = new List<VistaLocalidad>();

        var empleados = _context.Empleados.Include(l => l.Localidades).ToList();

        if (buscarLocalidad != 0)
        {
            empleados = empleados.Where(e => e.LocalidadID == buscarLocalidad).ToList();
        }

        foreach (var e in empleados)
        {
            var localidadMostar = localidadesMostrar.Where(l => l.LocalidadID == e.LocalidadID).SingleOrDefault();
            if (localidadMostar == null)
            {
                localidadMostar = new VistaLocalidad
                {
                    LocalidadID = e.LocalidadID,
                    NombreLocalidad = e.Localidades.NombreLocalidad,
                    ListadoEmpleados = new List<VistaEmpleado>()
                };
                localidadesMostrar.Add(localidadMostar);
            }

            var VistaEmpleado = new VistaEmpleado
            {
                EmpleadoID = e.EmpleadoID,
                LocalidadID = e.LocalidadID,
                Nombre = e.Nombre,
                NombreLocalidad = e.Localidades.NombreLocalidad,
                Apellido = e.Apellido,
                Nacimiento = e.Nacimiento.ToString("dd/MM/yyyy"),
                Telefono = e.Telefono,
                Email = e.Email,
                Salario = e.Salario,
                Direccion = e.Direccion
            };
            localidadMostar.ListadoEmpleados.Add(VistaEmpleado);
        }

        return Json(localidadesMostrar);
    }

    public IActionResult Informe()
    {
        return View();
    }

    public JsonResult ListadoInformesTres()
    {
        List<VistaSector> sectoresMostrar = new List<VistaSector>();

        var empleados = _context.Empleados.Include(l => l.Sectores).Include(s => s.Localidades).ToList();

        foreach (var e in empleados.OrderByDescending(s => s.Sectores.SectorID).OrderBy(l => l.Localidades.LocalidadID))
        {
            var sectorMostar = sectoresMostrar.Where(l => l.SectorID == e.SectorID).SingleOrDefault();
            if (sectorMostar == null)
            {
                sectorMostar = new VistaSector
                {
                    SectorID = e.SectorID,
                    NombreSector = e.Sectores.NombreSector,
                    ListadoLocalidades = new List<VistaLocalidad>()
                };
                sectoresMostrar.Add(sectorMostar);
            }

            var localidadMostrar = sectorMostar.ListadoLocalidades.Where(l => l.LocalidadID == e.LocalidadID).SingleOrDefault();
            if (localidadMostrar == null)
            {
                localidadMostrar = new VistaLocalidad
                {
                    LocalidadID = e.LocalidadID,
                    NombreLocalidad = e.Localidades.NombreLocalidad,
                    ListadoEmpleados = new List<VistaEmpleado>()
                };
                sectorMostar.ListadoLocalidades.Add(localidadMostrar);
            }

            var empleadoMostrar = localidadMostrar.ListadoEmpleados.Where(l => l.EmpleadoID == e.EmpleadoID).SingleOrDefault();
            if (empleadoMostrar == null)
            {
                empleadoMostrar = new VistaEmpleado
                {
                    EmpleadoID = e.EmpleadoID,
                    LocalidadID = e.LocalidadID,
                    Nombre = e.Nombre,
                    NombreLocalidad = e.Localidades.NombreLocalidad,
                    Apellido = e.Apellido,
                    Nacimiento = e.Nacimiento.ToString("dd/MM/yyyy"),
                    Telefono = e.Telefono,
                    Email = e.Email,
                    Salario = e.Salario,
                    Direccion = e.Direccion
                };
                localidadMostrar.ListadoEmpleados.Add(empleadoMostrar);
            }
        }

        return Json(sectoresMostrar);
    }
}

