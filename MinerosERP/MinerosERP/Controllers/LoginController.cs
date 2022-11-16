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

        public async Task<IActionResult> Index(bool isHacker = false)
        {
            //List<Empleados> empleados = await _serviciosEmpleadosAPI.Lista("empleado");
            // return View(/*empleados*/);
            ViewBag.log = isHacker;
            return View();
        }

        //[HttpPost]
        public async Task<IActionResult> Login(Login obj)
        {

            LoginResponse result = await _serviciosEmpleadosAPI.Login(obj);
            //Debug.WriteLine(result);         
                //Console.WriteLine(result);
            if (result.pk != 0 && result.username != null && result.username!="")
            {
                List<Empleados> Empleados = await _serviciosEmpleadosAPI.ListarEmpleados();
                Empleados currentEmpleado = Empleados.Where(x => x.id_usuario == result.pk).FirstOrDefault();
                HttpContext.Session.SetInt32("id_usuario", result.pk);
                HttpContext.Session.SetInt32("id_empleado", currentEmpleado.id_empleado);
                HttpContext.Session.SetString("username", result.username.ToString());
                string[] nombres = currentEmpleado.nombres_empleado.Split(' ');
                HttpContext.Session.SetString("full_name", $"{nombres[0]} {currentEmpleado.apellidos_empleado}".ToString());               
                return RedirectToAction("Index", "Home");
            }
            else
            {

                return RedirectToAction("Index", "Login", new { isHacker = true} );
            }
        }


        public async Task<IActionResult> Logout()
        {
            return RedirectToAction("Index", "Login");
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