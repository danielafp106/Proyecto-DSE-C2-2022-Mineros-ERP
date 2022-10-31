using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using MinerosERP.Models;

namespace MinerosERP.Services
{
    public class APIReutlizable : IServicio_API
    {
        private static string _baseurl;

        public APIReutlizable()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            _baseurl = builder.GetSection("ApiSettings:baseUrl").Value;

        }

        #region EMPLEADOS
        public async Task<List<Empleados>> ListarEmpleados()
        {
            List<Empleados> lista = new List<Empleados>();
            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseurl);
            var response = await cliente.GetAsync("api/empleado");
            if (response.IsSuccessStatusCode)
            {
                var json_repuesta = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<List<Empleados>>(json_repuesta);
                lista.AddRange(resultado);
            }
            return lista;
        }

        public async Task<Empleados> ObtenerEmpleado(int id)
        {
            Empleados empleado = new Empleados();
            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseurl);
            var response = await cliente.GetAsync($"api/empleado/{id}");
            if (response.IsSuccessStatusCode)
            {
                var json_repuesta = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<Empleados>(json_repuesta);
                empleado = resultado;
            }
            return empleado;
        }

        public async Task<bool> GuardarEmpleado(Empleados EmpObjeto)
        {
            bool resultado = false;
            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseurl);

            var content = new StringContent(JsonConvert.SerializeObject(EmpObjeto), Encoding.UTF8, "application/json");
            var response = await cliente.PostAsync($"api/empleado", content);

            if (response.IsSuccessStatusCode)
            {
                resultado = true;
            }

            return resultado;
        }

        public async Task<bool> EditarEmpleado(int id, Empleados EmpObjeto)
        {
            bool resultado = false;
            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseurl);

            var content = new StringContent(JsonConvert.SerializeObject(EmpObjeto), Encoding.UTF8, "application/json");
            var response = await cliente.PutAsync($"api/empleado/{id}", content);

            if(response.IsSuccessStatusCode)
            {
                resultado = true;
            }

            return resultado;
        }

        public async Task<bool> EliminarEmpleado(int id)
        {
            bool resultado = false;
            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseurl);

            var response = await cliente.DeleteAsync($"api/empleado/{id}");

            if (response.IsSuccessStatusCode)
            {
                resultado = true;
            }

            return resultado;
        }
        #endregion

        #region MARCACIONES
        public async Task<List<Marcaciones>> ListarMarcaciones()
        {
            List<Marcaciones> lista = new List<Marcaciones>();
            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseurl);
            var response = await cliente.GetAsync("api/marcaciones");
            if (response.IsSuccessStatusCode)
            {
                var json_repuesta = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<List<Marcaciones>>(json_repuesta);
                lista.AddRange(resultado);
            }
            return lista;
        }

        public async Task<Marcaciones> ObtenerMarcacion(int id)
        {
            Marcaciones objeto = new Marcaciones();
            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseurl);
            var response = await cliente.GetAsync($"api/marcaciones/{id}");
            if (response.IsSuccessStatusCode)
            {
                var json_repuesta = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<Marcaciones>(json_repuesta);
                objeto = resultado;
            }
            return objeto;
        }

        public async Task<bool> GuardarMarcacion(Marcaciones Objeto)
        {
            bool resultado = false;
            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseurl);

            var content = new StringContent(JsonConvert.SerializeObject(Objeto), Encoding.UTF8, "application/json");
            var response = await cliente.PostAsync($"api/marcaciones", content);

            if (response.IsSuccessStatusCode)
            {
                resultado = true;
            }

            return resultado;
        }

        public async Task<bool> EditarMarcacion(int id, Marcaciones Objeto)
        {
            bool resultado = false;
            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseurl);

            var content = new StringContent(JsonConvert.SerializeObject(Objeto), Encoding.UTF8, "application/json");
            var response = await cliente.PutAsync($"api/marcaciones/{id}", content);

            if (response.IsSuccessStatusCode)
            {
                resultado = true;
            }

            return resultado;
        }

        public async Task<bool> EliminarMarcacion(int id)
        {
            bool resultado = false;
            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseurl);

            var response = await cliente.DeleteAsync($"api/marcaciones/{id}");

            if (response.IsSuccessStatusCode)
            {
                resultado = true;
            }

            return resultado;
        }
        #endregion

    }
}
