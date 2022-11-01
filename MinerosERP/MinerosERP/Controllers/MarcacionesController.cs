using Microsoft.AspNetCore.Mvc;
using MinerosERP.Models;
using MinerosERP.Services;
using System.Globalization;

namespace MinerosERP.Controllers
{
    public class MarcacionesController : Controller
    {
        private readonly IServicio_API _serviciosEmpleadosAPI;

        public MarcacionesController(IServicio_API servicioEmpleadosAPI)
        {
            _serviciosEmpleadosAPI = servicioEmpleadosAPI;
        }

        public async Task<IActionResult> Index()
        {
            bool HayMarcacionAbierta = false;
            DateTime LunesDeSemanaActual = DateTime.Today;
            while(LunesDeSemanaActual.DayOfWeek != DayOfWeek.Monday)
            {
                LunesDeSemanaActual = LunesDeSemanaActual.AddDays(-1);
            }
            DateTime DomingoDeSemanaActual = LunesDeSemanaActual.AddDays(7);

            //Sumador de horas de la semana actual
            TimeSpan totalSemana = new TimeSpan(); 

            //Obtenemos todos los registros del usuario
            List<Marcaciones> marcaciones = await _serviciosEmpleadosAPI.ListarMarcaciones();
            List<Marcaciones> semanaactual = new List<Marcaciones>();
           // List<Marcaciones> semanaactual = new List<Marcaciones>();
            var listaOrdenada = from i in marcaciones
                                orderby DateTime.ParseExact(i.fecha_marcacion+" "+i.hora_entrada, "yyyy-M-dd hh:mm:ss tt", CultureInfo.InvariantCulture) descending
                                select i;
            marcaciones = listaOrdenada.ToList();

            //Sacando horas trabajadas x cada registro
            foreach (var item in marcaciones.ToList())
            {          
                DateTime horaEntrada = DateTime.ParseExact($"{item.fecha_marcacion} {item.hora_entrada}", "yyyy-M-dd hh:mm:ss tt", CultureInfo.InvariantCulture);
                DateTime horaSalida = DateTime.ParseExact($"{item.fecha_marcacion} {item.hora_salida}", "yyyy-M-dd hh:mm:ss tt", CultureInfo.InvariantCulture);
                
                
                int format = 11 - (horaEntrada.ToString("dddd")).Length;
                item.fecha_marcacion = horaEntrada.ToString("ddd").PadRight(5)+" "+horaEntrada.ToString("dd/M/yyyy");
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
                    if(result.Hours > 5)
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
        public ActionResult MarcarEntradaModal()
        {
            Marcaciones obj = new Marcaciones();
            obj.fecha_marcacion = DateTime.Today.ToString("yyyy-M-dd");
            obj.hora_entrada = DateTime.Now.ToString("hh:mm tt", CultureInfo.InvariantCulture);
            obj.hora_salida = obj.hora_entrada;
            obj.id_empleado = 2;
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
    }
}
