using Microsoft.AspNetCore.Mvc;
using MinerosERP.Models;
using MinerosERP.Services;
using System.Diagnostics;

namespace MinerosERP.Controllers
{
    public class RegistrationController : Controller


    {
        private readonly IServicio_API _serviciosEmpleadosAPI;

        public RegistrationController(IServicio_API servicioEmpleadosAPI)
        {
            _serviciosEmpleadosAPI = servicioEmpleadosAPI;
        }

        // GET: RegistrationController
        public ActionResult Registration()
        {
            return View();
        }


        public async Task<IActionResult> Register(Registration obj)
        {



            if(obj.password1 != obj.password2)
            {

                //Aqui debe mostrar una alerta de que las contraseñas deben ser iguales.
                return RedirectToAction("Registration", "Registration");
            }


            string resul = await _serviciosEmpleadosAPI.Register(obj);




            if (resul == "true")
            {
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Registration", "Registration");
        }

        // GET: RegistrationController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: RegistrationController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RegistrationController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: RegistrationController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: RegistrationController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: RegistrationController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: RegistrationController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
