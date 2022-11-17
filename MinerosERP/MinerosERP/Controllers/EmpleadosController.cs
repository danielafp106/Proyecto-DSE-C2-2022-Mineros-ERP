﻿using Microsoft.AspNetCore.Mvc;
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
            if (TempData["resultado"] != null && TempData["mensajeResultado"] != null)
            {
                ViewBag.resultado = Convert.ToBoolean(TempData["resultado"]);
                ViewBag.mensajeResultado = TempData["mensajeResultado"].ToString();
                if (ViewBag.usermessage != null)
                {
                    ViewBag.usermessage = TempData["usermessage"].ToString();
                }        
            }

            //Obtenemos todos los registros del usuario
            List<Empleados> empleados = await _serviciosEmpleadosAPI.ListarEmpleados();
            List<Areas> areaEmp = await _serviciosEmpleadosAPI.ListarAreas();
            List<Cargos> cargoEmp = await _serviciosEmpleadosAPI.ListarCargosEmpleados();
            List<Usuarios> allusers = await _serviciosEmpleadosAPI.ListarUsuarios();
            ViewBag.areas = areaEmp;
            ViewBag.cargos = cargoEmp;
            foreach(Empleados emp in empleados)
            {
                emp.usuario = allusers.Where(x => x.id == emp.id_usuario).Select(x => x.username).FirstOrDefault();
            }
            var listaOrdenada = from i in empleados
                                orderby i.nombres_empleado ascending, i.apellidos_empleado ascending
                                select i;
            empleados = listaOrdenada.ToList();
            return View(empleados);
        }
        public async Task<IActionResult> RegistrarEmpleadoModal()
        {
            Empleados obj = new Empleados();
            List<Areas> areaEmp = await _serviciosEmpleadosAPI.ListarAreas();
            List<Cargos> cargoEmp = await _serviciosEmpleadosAPI.ListarCargosEmpleados();
            List<Roles> rolesEmp = await _serviciosEmpleadosAPI.ListarRoles();
            ViewBag.areas = areaEmp;
            ViewBag.cargos = cargoEmp;
            ViewBag.roles = rolesEmp;

            return PartialView("RegistrarEmpleadoModal", obj);
        }
        public async Task<IActionResult> EditarEmpleadoModal(int id)
        {
            Empleados obj = await _serviciosEmpleadosAPI.ObtenerEmpleado(id);
            List<Areas> areaEmp = await _serviciosEmpleadosAPI.ListarAreas();
            List<Cargos> cargoEmp = await _serviciosEmpleadosAPI.ListarCargosEmpleados();
            Usuarios user = await _serviciosEmpleadosAPI.ObtenerUsuario(obj.id_usuario);
            obj.usuario = user.username;
            obj.email = user.email;
            obj.passwordtemp = "jaja";

            ViewBag.areas = areaEmp;
            ViewBag.cargos = cargoEmp;


            return PartialView("EditarEmpleadoModal", obj);
        }

        public async Task<IActionResult> VerEmpleadoModal(int id)
        {
            Empleados obj = await _serviciosEmpleadosAPI.ObtenerEmpleado(id);
            List<Areas> areaEmp = await _serviciosEmpleadosAPI.ListarAreas();
            List<Cargos> cargoEmp = await _serviciosEmpleadosAPI.ListarCargosEmpleados();
            Usuarios user = await _serviciosEmpleadosAPI.ObtenerUsuario(obj.id_usuario);
            obj.usuario = user.username;
            obj.email = user.email;
            obj.passwordtemp = "jaja";

            ViewBag.areas = areaEmp;
            ViewBag.cargos = cargoEmp;
            if (id == Convert.ToInt32(HttpContext.Session.GetInt32("id_empleado").ToString()))
            {
                ViewBag.Title = "Mi Ficha de Empleado";
            }
            else {
                ViewBag.Title = "Ficha de Empleado";
            }



            return PartialView("VerEmpleadoModal", obj);
        }
        public async Task<IActionResult> GuardarEmpleado(Empleados obj)
        {
            if(ModelState.IsValid)
            {
                List<Usuarios> allusers = await _serviciosEmpleadosAPI.ListarUsuarios();
                int count = allusers.Where(x => x.username.Contains(obj.usuario)).ToList().Count;
                if (count!=0)
                {
                    string usermessage = $"El usuario {obj.usuario} ya existia en el sistema, por lo tanto,";
                    obj.usuario = $"{obj.usuario}{count}";
                    usermessage =  $"{usermessage} el usuario registrado para este empleado es {obj.usuario}";
                    TempData["usermessage"] = usermessage;
                }
                count = allusers.Where(x => x.email == obj.email).ToList().Count;
                if(count !=0)
                {
                    obj.email = null;
                    throw new Exception("Este correo pertenece a otro empleado de la empresa, favor digitar uno válido.");
                }
                Registration nuevoUsuario = new Registration()
                {
                    username = obj.usuario,
                    email = obj.email,
                    password1 = obj.passwordtemp,
                    password2 = obj.passwordtemp
                };
                if(await _serviciosEmpleadosAPI.Register(nuevoUsuario))
                {
                    allusers = await _serviciosEmpleadosAPI.ListarUsuarios();
                    int idusuario = allusers.Where(x => x.username == nuevoUsuario.username).Select(x => x.id).FirstOrDefault();
                    obj.id_usuario = idusuario;
                }
                else
                {
                    throw new Exception("Algo salió mal, intentelo de nuevo.");
                }
                TempData["resultado"] = await _serviciosEmpleadosAPI.GuardarEmpleado(obj);
                TempData["mensajeResultado"] = "¡Empleado Registrado Exitosamente!";
                return RedirectToAction("Index");
            }
            throw new Exception("Existen campos vacíos, porfavor completar formulario.");

        }
        public async Task<IActionResult> EditarEmpleado(int id, Empleados obj)
        {
            if (ModelState.IsValid)
            {
                ViewBag.resultado = await _serviciosEmpleadosAPI.EditarEmpleado(id, obj);
                return RedirectToAction("Index");
            }
            throw new Exception("Existen campos vacíos, porfavor completar formulario.");

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
