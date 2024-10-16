
using Microsoft.AspNetCore.Mvc;
using Recuperatorio.Data;
using Recuperatorio.Models;

namespace Recuperatorio.Controllers;



public class SectoresController : Controller
{
    private ApplicationDbContext _context;
    public SectoresController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        return View();
    }

        public JsonResult ListadoSectores (int? SectorID)
    {
        var listadoSectores = _context.Sectores.ToList();
            listadoSectores = _context.Sectores.OrderBy(l => l.NombreSector).ToList();

        if(SectorID != null)
        {
            listadoSectores = _context.Sectores.Where(l => l.SectorID == SectorID).ToList();
        }
        return Json(listadoSectores);
    }

    public JsonResult GuardarSector (int SectorID, string NombreSector)
    {
        string resultado = "";

        NombreSector = NombreSector.ToUpper();

        if(SectorID == 0)
        {
            var nuevoSector = new Sector
            {
                NombreSector = NombreSector
            };
            _context.Add(nuevoSector);
            _context.SaveChanges();
            resultado = "Sector guardada correctamente";
        }
        else
        {
            var editarSector = _context.Sectores.Where(e => e.SectorID == SectorID).SingleOrDefault();
            
            if(editarSector != null)
            {
                editarSector.NombreSector = NombreSector;
                _context.SaveChanges();
                resultado = "Sector editada correctamente";
            }
        }
        return Json(resultado);
    }

    public JsonResult EliminarSector(int SectorID)
    {
        var eliminarSector = _context.Sectores.Find(SectorID);
        _context.Remove(eliminarSector);
        _context.SaveChanges();

        return Json(eliminarSector);
    }
}