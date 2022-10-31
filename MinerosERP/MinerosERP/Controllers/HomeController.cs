using Microsoft.AspNetCore.Mvc;
using MinerosERP.Models;
using MinerosERP.Services;
using System.Diagnostics;

namespace MinerosERP.Controllers
{
    public class HomeController : Controller
    {
        private readonly IServicio_API _serviciosEmpleadosAPI;

        public HomeController(IServicio_API servicioEmpleadosAPI)
        {
            _serviciosEmpleadosAPI = servicioEmpleadosAPI;
        }

        public async Task<IActionResult> Index()
        {
            //List<Empleados> empleados = await _serviciosEmpleadosAPI.Lista("empleado");
            return View(/*empleados*/);
        }

        public async Task<IActionResult> Empleado(int id)
        {
            Empleados empleado = new Empleados();
            if(id != 0)
            {
               // empleado = await _serviciosEmpleadosAPI.Obtener("empleado",id);
            }
            return View(empleado);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}