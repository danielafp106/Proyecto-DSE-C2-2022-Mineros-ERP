using Microsoft.AspNetCore.Mvc;
using MinerosERP.Models;
using MinerosERP.Services;
using System.Globalization;
namespace MinerosERP.Controllers
{
    public class EmpleadosController : Controller
    {
    
        private readonly IServicio_API _serviciosEmpleadosAPI;

        public EmpleadosController(IServicio_API servicioEmpleadosAPI)
        {
            _serviciosEmpleadosAPI = servicioEmpleadosAPI;
        }

        public async Task<IActionResult> Index()
        {
            
           
            //Obtenemos todos los registros del usuario
            List<Empleados> empleados = await _serviciosEmpleadosAPI.ListarEmpleados();
            //List<Empleados> listadoEmpleados = new List<Empleados>();

            return View(empleados);
        }
        public ActionResult RegistrarEmpleadoModal()
        {
            Empleados obj = new Empleados();    
           
            return PartialView("RegistrarEmpleadoModal", obj);
        }
        public async Task<IActionResult> GuardarEmpleado(Empleados obj)
        {
           
            ViewBag.resultado = await _serviciosEmpleadosAPI.GuardarEmpleado(obj);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> MarcarSalida(int id)
        {
            Marcaciones obj = await _serviciosEmpleadosAPI.ObtenerMarcacion(id);
            obj.hora_salida = DateTime.Now.ToString("hh:mm:ss tt", CultureInfo.InvariantCulture);
            ViewBag.resultado = await _serviciosEmpleadosAPI.EditarMarcacion(id, obj);
            return RedirectToAction("Index");
        }
    }
}
