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
            List<Areas> areaEmp = await _serviciosEmpleadosAPI.ListarAreas();
            List<Cargos> cargoEmp = await _serviciosEmpleadosAPI.ListarCargosEmpleados();
            ViewBag.areas = areaEmp;
            ViewBag.cargos = cargoEmp;
            //List<Empleados> listadoEmpleados = new List<Empleados>();

            return View(empleados);
        }
        public async Task<IActionResult> RegistrarEmpleadoModal()
        {
            Empleados obj = new Empleados();
            List<Areas> areaEmp = await _serviciosEmpleadosAPI.ListarAreas();
            List<Cargos> cargoEmp = await _serviciosEmpleadosAPI.ListarCargosEmpleados();
            ViewBag.areas = areaEmp;
            ViewBag.cargos = cargoEmp;

            return PartialView("RegistrarEmpleadoModal", obj);
        }
        public async Task<IActionResult> EditarEmpleadoModal(int id)
        {
            Empleados obj = await _serviciosEmpleadosAPI.ObtenerEmpleado(id);
            List<Areas> areaEmp = await _serviciosEmpleadosAPI.ListarAreas();
            List<Cargos> cargoEmp = await _serviciosEmpleadosAPI.ListarCargosEmpleados();
            ViewBag.areas = areaEmp;
            ViewBag.cargos = cargoEmp;

            return PartialView("EditarEmpleadoModal", obj);
        }
        public async Task<IActionResult> GuardarEmpleado(Empleados obj)
        {
            if(ModelState.IsValid)
            {
                ViewBag.resultado = await _serviciosEmpleadosAPI.GuardarEmpleado(obj);
                return RedirectToAction("Index");
            }
            return View(obj);
            
        }
        public async Task<IActionResult> EditarEmpleado(int id, Empleados obj)
        {
                       
            ViewBag.resultado = await _serviciosEmpleadosAPI.EditarEmpleado(id, obj);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> EliminarEmpleado(int id)
        {

            var resultado = await _serviciosEmpleadosAPI.EliminarEmpleado(id);
            if (resultado)
                return RedirectToAction("Index");
            else
                return NoContent();
        }


    }
}
