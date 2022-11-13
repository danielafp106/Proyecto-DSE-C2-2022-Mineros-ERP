using Microsoft.AspNetCore.Mvc;
using MinerosERP.Models;
using MinerosERP.Services;
using System.Globalization;

namespace MinerosERP.Controllers
{
    
    public class MarcacionesController : Controller
    {
        private readonly IServicio_API _serviciosEmpleadosAPI;
        public string username = "admin";
        public int pk = 2;

        public MarcacionesController(IServicio_API servicioEmpleadosAPI)
        {
            _serviciosEmpleadosAPI = servicioEmpleadosAPI;
        }

        public async Task<IActionResult> Index()
        {
            try{
                Convert.ToInt32(HttpContext.Session.GetInt32("id_empleado").ToString());
                bool HayMarcacionAbierta = false;
                DateTime LunesDeSemanaActual = DateTime.Today;
                while (LunesDeSemanaActual.DayOfWeek != DayOfWeek.Monday)
                {
                    LunesDeSemanaActual = LunesDeSemanaActual.AddDays(-1);
                }
                DateTime DomingoDeSemanaActual = LunesDeSemanaActual.AddDays(7);

                //Sumador de horas de la semana actual
                TimeSpan totalSemana = new TimeSpan();

                //Obtenemos todos los registros del usuario
                List<Marcaciones> marcaciones = await _serviciosEmpleadosAPI.ListarMarcaciones();
                marcaciones = marcaciones.Where(x => x.id_empleado == Convert.ToInt32(HttpContext.Session.GetInt32("id_empleado").ToString())).ToList();
                List<Marcaciones> semanaactual = new List<Marcaciones>();
                // List<Marcaciones> semanaactual = new List<Marcaciones>();
                var listaOrdenada = from i in marcaciones
                                    orderby DateTime.ParseExact(i.fecha_marcacion + " " + i.hora_entrada, "yyyy-M-dd hh:mm:ss tt", CultureInfo.InvariantCulture) descending
                                    select i;
                marcaciones = listaOrdenada.ToList();

                //Sacando horas trabajadas x cada registro
                foreach (var item in marcaciones.ToList())
                {
                    DateTime horaEntrada = DateTime.ParseExact($"{item.fecha_marcacion} {item.hora_entrada}", "yyyy-M-dd hh:mm:ss tt", CultureInfo.InvariantCulture);
                    DateTime horaSalida = DateTime.ParseExact($"{item.fecha_marcacion} {item.hora_salida}", "yyyy-M-dd hh:mm:ss tt", CultureInfo.InvariantCulture);
                    item.username = HttpContext.Session.GetString("username").ToString();

                    int format = 11 - (horaEntrada.ToString("dddd")).Length;
                    item.fecha_marcacion = horaEntrada.ToString("ddd").PadRight(5) + " " + horaEntrada.ToString("dd/M/yyyy");
                    item.hora_entrada = horaEntrada.ToString("hh:mm tt");
                    item.hora_salida = horaSalida.ToString("hh:mm tt");
                    if (horaEntrada == horaSalida)
                    {
                        HayMarcacionAbierta = true;
                        ViewBag.IdMarcacionAbierta = item.id_marcacion;
                        item.hora_salida = "";
                        if (horaEntrada >= LunesDeSemanaActual && horaEntrada <= DomingoDeSemanaActual)
                        {
                            semanaactual.Add(item);
                            marcaciones.RemoveAll(i => i.id_marcacion == item.id_marcacion);
                        }
                    }
                    else
                    {
                        TimeSpan result = horaSalida.Subtract(horaEntrada);
                        if (result.Hours > 5)
                        {
                            result = result.Subtract(new TimeSpan(0, 60, 0)); //Restando Hora de almuerzo     
                        }
                        item.total_horas = $"{result:%h} horas {result:%m} minutos";

                        if (horaEntrada >= LunesDeSemanaActual && horaEntrada <= DomingoDeSemanaActual)
                        {
                            totalSemana = totalSemana + result;
                            semanaactual.Add(item);
                            marcaciones.RemoveAll(i => i.id_marcacion == item.id_marcacion);
                        }
                    }


                }
                ViewBag.HayMarcacionAbierta = HayMarcacionAbierta;
                ViewBag.TodasMarcaciones = marcaciones;
                ViewBag.HorasTotalesSemana = $"{(int)totalSemana.TotalHours} horas {totalSemana.ToString(@"mm")} minutos";
                return View(semanaactual);
            }
            catch
            {
                return RedirectToAction("Index","Login");
            }

            
        }
        public async Task<IActionResult> IndexJefes()
        {
            try
            {
                int currentIdEmpleado = 2;//Convert.ToInt32(HttpContext.Session.GetInt32("id_empleado").ToString());
                Empleados currenEmpleado = await _serviciosEmpleadosAPI.ObtenerEmpleado(currentIdEmpleado);
                bool HayMarcacionAbierta = false;
                DateTime LunesDeSemanaActual = DateTime.Today;
                while (LunesDeSemanaActual.DayOfWeek != DayOfWeek.Monday)
                {
                    LunesDeSemanaActual = LunesDeSemanaActual.AddDays(-1);
                }
                DateTime DomingoDeSemanaActual = LunesDeSemanaActual.AddDays(7);

                //Sumador de horas de la semana actual
                TimeSpan totalSemana = new TimeSpan();

                //Obtenemos todos los registros del usuario
                List<Marcaciones> marcaciones = await _serviciosEmpleadosAPI.ListarMarcaciones();
                // marcaciones = marcaciones.Where(x => x.id_empleado == Convert.ToInt32(HttpContext.Session.GetInt32("id_usuario").ToString())).ToList();
                List<Marcaciones> semanaactual = new List<Marcaciones>();
                List<Marcaciones> semanaactualEmpleados = new List<Marcaciones>();
                List<Marcaciones> marcacionesEmpleados = new List<Marcaciones>();
                // List<Marcaciones> semanaactual = new List<Marcaciones>();
                var listaOrdenada = from i in marcaciones
                                    orderby DateTime.ParseExact(i.fecha_marcacion + " " + i.hora_entrada, "yyyy-M-dd hh:mm:ss tt", CultureInfo.InvariantCulture) descending
                                    select i;
                marcaciones = listaOrdenada.ToList();
                List<Empleados> allEmpleados = await _serviciosEmpleadosAPI.ListarEmpleados();
                allEmpleados = allEmpleados.Where(x=>x.id_area == currenEmpleado.id_area).ToList();
                List<Usuarios> allUsuarios = await _serviciosEmpleadosAPI.ListarUsuarios();
                List<SelectEmpleados> ListEmpleados = new List<SelectEmpleados>();
                //Sacando horas trabajadas x cada registro
                foreach (var item in marcaciones.ToList())
                {
                    DateTime horaEntrada = DateTime.ParseExact($"{item.fecha_marcacion} {item.hora_entrada}", "yyyy-M-dd hh:mm:ss tt", CultureInfo.InvariantCulture);
                    DateTime horaSalida = DateTime.ParseExact($"{item.fecha_marcacion} {item.hora_salida}", "yyyy-M-dd hh:mm:ss tt", CultureInfo.InvariantCulture);
                    item.username = username;

                    int format = 11 - (horaEntrada.ToString("dddd")).Length;
                    item.fecha_marcacion = horaEntrada.ToString("ddd").PadRight(5) + " " + horaEntrada.ToString("dd/M/yyyy");
                    item.hora_entrada = horaEntrada.ToString("hh:mm tt");
                    item.hora_salida = horaSalida.ToString("hh:mm tt");
                    if(item.id_empleado == currentIdEmpleado)
                    {
                        if (horaEntrada == horaSalida)
                        {
                            HayMarcacionAbierta = true;
                            ViewBag.IdMarcacionAbierta = item.id_marcacion;
                            item.hora_salida = "";
                            if (horaEntrada >= LunesDeSemanaActual && horaEntrada <= DomingoDeSemanaActual)
                            {
                                semanaactual.Add(item);
                                marcaciones.RemoveAll(i => i.id_marcacion == item.id_marcacion);
                            }
                        }
                        else
                        {
                            TimeSpan result = horaSalida.Subtract(horaEntrada);
                            if (result.Hours > 5)
                            {
                                result = result.Subtract(new TimeSpan(0, 60, 0)); //Restando Hora de almuerzo     
                            }
                            item.total_horas = $"{result:%h} horas {result:%m} minutos";

                            if (horaEntrada >= LunesDeSemanaActual && horaEntrada <= DomingoDeSemanaActual)
                            {
                                totalSemana = totalSemana + result;
                                semanaactual.Add(item);
                                marcaciones.RemoveAll(i => i.id_marcacion == item.id_marcacion);
                            }
                        }
                    }
                    else
                    {
                        if(Convert.ToInt32(allEmpleados.Where(x => x.id_empleado == item.id_empleado).Select(x=>x.id_area).FirstOrDefault())== currenEmpleado.id_area)
                        {              
                            int id_usuario = Convert.ToInt32(allEmpleados.Where(x => x.id_empleado == item.id_empleado).Select(x => x.id_usuario).FirstOrDefault());
                            string usuario = allUsuarios.Where(x => x.id == id_usuario).Select(x => x.username).FirstOrDefault().ToString();
                            item.username = usuario;                           
                            if (horaEntrada == horaSalida)
                            {
                                item.hora_salida = "";
                                if (horaEntrada >= LunesDeSemanaActual && horaEntrada <= DomingoDeSemanaActual)
                                {
                                    semanaactualEmpleados.Add(item);
                                    marcaciones.RemoveAll(i => i.id_marcacion == item.id_marcacion);
                                }
                            }
                            else
                            {
                                TimeSpan result = horaSalida.Subtract(horaEntrada);
                                if (result.Hours > 5)
                                {
                                    result = result.Subtract(new TimeSpan(0, 60, 0)); //Restando Hora de almuerzo     
                                }
                                item.total_horas = $"{result:%h} horas {result:%m} minutos";

                                if (horaEntrada >= LunesDeSemanaActual && horaEntrada <= DomingoDeSemanaActual)
                                {
                                    semanaactualEmpleados.Add(item);
                                    marcaciones.RemoveAll(i => i.id_marcacion == item.id_marcacion);
                                }
                                else
                                {
                                    marcacionesEmpleados.Add(item);
                                    marcaciones.RemoveAll(i => i.id_marcacion == item.id_marcacion);
                                }
                            }
                        }
                        else
                        {
                            marcaciones.RemoveAll(i => i.id_marcacion == item.id_marcacion);
                        }
                        
                    }
                    


                }

                foreach(Empleados item in allEmpleados)
                {
                    if(item.id_empleado != currentIdEmpleado)
                    {
                        ListEmpleados.Add(new SelectEmpleados()
                        {
                            id_empleado = item.id_empleado,
                            username = allUsuarios.Where(x => x.id == item.id_usuario).Select(x => x.username).FirstOrDefault().ToString()
                        });
                    }                    
                }
                ViewBag.AllEmpleados = ListEmpleados.DistinctBy(x=>x.id_empleado);
                ViewBag.HayMarcacionAbierta = HayMarcacionAbierta;
                ViewBag.TodasMarcaciones = marcaciones;
                ViewBag.MarcacionesEmpleados = marcacionesEmpleados;
                ViewBag.SemanaActualEmpleados = semanaactualEmpleados;
                ViewBag.HorasTotalesSemana = $"{(int)totalSemana.TotalHours} horas {totalSemana.ToString(@"mm")} minutos";
                return View(semanaactual);
            }
            catch
            {
                return RedirectToAction("Index", "Login");
            }


        }
        public ActionResult MarcarEntradaModal()
        {
            Marcaciones obj = new Marcaciones();
            obj.fecha_marcacion = DateTime.Today.ToString("yyyy-M-dd");
            obj.hora_entrada = DateTime.Now.ToString("hh:mm tt", CultureInfo.InvariantCulture);
            obj.hora_salida = obj.hora_entrada;
            obj.id_empleado = Convert.ToInt32(HttpContext.Session.GetInt32("id_empleado").ToString());
            return PartialView("MarcarEntradaModal", obj);
        }
        public async Task<IActionResult> GuardarMarcacion(Marcaciones obj)
        {
            obj.fecha_marcacion = DateTime.Today.ToString("yyyy-M-dd");
            obj.hora_entrada = DateTime.Now.ToString("hh:mm:ss tt", CultureInfo.InvariantCulture);
            obj.hora_salida = obj.hora_entrada;
            ViewBag.resultado = await _serviciosEmpleadosAPI.GuardarMarcacion(obj);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> MarcarSalida(int id)
        {
            Marcaciones obj = await _serviciosEmpleadosAPI.ObtenerMarcacion(id);
            obj.hora_salida = DateTime.Now.ToString("hh:mm:ss tt", CultureInfo.InvariantCulture);
            ViewBag.resultado = await _serviciosEmpleadosAPI.EditarMarcacion(id, obj);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> VerHistorial(int id)
        {
            List<Marcaciones> obj = await _serviciosEmpleadosAPI.ListarMarcaciones();
            return PartialView("VerHistorial");
        }
       
    }
}
