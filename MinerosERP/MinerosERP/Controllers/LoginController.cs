using Microsoft.AspNetCore.Mvc;
using MinerosERP.Models;
using MinerosERP.Services;
using System.Diagnostics;
using Microsoft.AspNetCore.Http;

namespace MinerosERP.Controllers
{
    public class LoginController : Controller
    {
        private readonly IServicio_API _serviciosEmpleadosAPI;

        public LoginController(IServicio_API servicioEmpleadosAPI)
        {
            _serviciosEmpleadosAPI = servicioEmpleadosAPI;
        }

        public async Task<IActionResult> Index()
        {
            //List<Empleados> empleados = await _serviciosEmpleadosAPI.Lista("empleado");
            // return View(/*empleados*/);
            return View();
        }

        //[HttpPost]
        public async Task<IActionResult> Login(Login obj)
        {

            LoginResponse result = await _serviciosEmpleadosAPI.Login(obj);

            if (result.pk != 0 && result.username != null && result.username!="")
            {
                HttpContext.Session.SetInt32("id_usuario", result.pk);
                HttpContext.Session.SetString("username", result.username.ToString());
                HttpContext.Session.SetString("full_name", $"{result.first_name} {result.last_name}".ToString());               
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        public async Task<IActionResult> Empleado(int id)
        {
            Empleados empleado = new Empleados();
            if (id != 0)
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