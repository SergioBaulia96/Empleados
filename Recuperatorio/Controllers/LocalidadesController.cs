using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Recuperatorio.Data;
using Recuperatorio.Models;

namespace Recuperatorio.Controllers;



public class LocalidadesController : Controller
{
    private ApplicationDbContext _context;
    public LocalidadesController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        return View();
    }

        public JsonResult ListadoLocalidades (int? LocalidadID)
    {
        var listadoLocalidades = _context.Localidades.ToList();
            listadoLocalidades = _context.Localidades.OrderBy(l => l.NombreLocalidad).ToList();

        if(LocalidadID != null)
        {
            listadoLocalidades = _context.Localidades.Where(l => l.LocalidadID == LocalidadID).ToList();
        }
        return Json(listadoLocalidades);
    }

    public JsonResult GuardarLocalidad (int LocalidadID, string NombreLocalidad)
    {
        string resultado = "";

        NombreLocalidad = NombreLocalidad.ToUpper();

        if(LocalidadID == 0)
        {
            var nuevaLocalidad = new Localidad
            {
                NombreLocalidad = NombreLocalidad
            };
            _context.Add(nuevaLocalidad);
            _context.SaveChanges();
            resultado = "Localidad guardada correctamente";
        }
        else
        {
            var editarLocalidad = _context.Localidades.Where(e => e.LocalidadID == LocalidadID).SingleOrDefault();
            
            if(editarLocalidad != null)
            {
                editarLocalidad.NombreLocalidad = NombreLocalidad;
                _context.SaveChanges();
                resultado = "Localidad editada correctamente";
            }
        }
        return Json(resultado);
    }

    public JsonResult EliminarLocalidad(int LocalidadID)
    {
        var eliminarLocalidad = _context.Localidades.Find(LocalidadID);
        _context.Remove(eliminarLocalidad);
        _context.SaveChanges();

        return Json(eliminarLocalidad);
    }
}