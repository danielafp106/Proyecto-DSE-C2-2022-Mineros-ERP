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
            var response = await cliente.GetAsync("api/empleados");
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
            var response = await cliente.GetAsync($"api/empleados/{id}");
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
            var response = await cliente.PostAsync($"api/empleados", content);

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
            var response = await cliente.PutAsync($"api/empleados/{id}", content);

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

            var response = await cliente.DeleteAsync($"api/empleados/{id}");

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

        #region AREAS
        public async Task<List<Areas>> ListarAreas()
        {
            List<Areas> lista = new List<Areas>();
            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseurl);
            var response = await cliente.GetAsync("api/areas");
            if (response.IsSuccessStatusCode)
            {
                var json_repuesta = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<List<Areas>>(json_repuesta);
                lista.AddRange(resultado);
            }
            return lista;
        }

        public async Task<Areas> ObtenerArea(int id)
        {
            Areas objeto = new Areas();
            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseurl);
            var response = await cliente.GetAsync($"api/areas/{id}");
            if (response.IsSuccessStatusCode)
            {
                var json_repuesta = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<Areas>(json_repuesta);
                objeto = resultado;
            }
            return objeto;
        }

        public async Task<bool> GuardarArea(Areas Objeto)
        {
            bool resultado = false;
            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseurl);

            var content = new StringContent(JsonConvert.SerializeObject(Objeto), Encoding.UTF8, "application/json");
            var response = await cliente.PostAsync($"api/areas", content);

            if (response.IsSuccessStatusCode)
            {
                resultado = true;
            }

            return resultado;
        }

        public async Task<bool> EditarArea(int id, Areas Objeto)
        {
            bool resultado = false;
            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseurl);

            var content = new StringContent(JsonConvert.SerializeObject(Objeto), Encoding.UTF8, "application/json");
            var response = await cliente.PutAsync($"api/areas/{id}", content);

            if (response.IsSuccessStatusCode)
            {
                resultado = true;
            }

            return resultado;
        }

        public async Task<bool> EliminarArea(int id)
        {
            bool resultado = false;
            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseurl);

            var response = await cliente.DeleteAsync($"api/areas/{id}");

            if (response.IsSuccessStatusCode)
            {
                resultado = true;
            }

            return resultado;
        }
        #endregion

        #region CARGOSEMPLEADOS
        public async Task<List<Cargos>> ListarCargosEmpleados()
        {
            List<Cargos> lista = new List<Cargos>();
            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseurl);
            var response = await cliente.GetAsync("api/cargos");
            if (response.IsSuccessStatusCode)
            {
                var json_repuesta = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<List<Cargos>>(json_repuesta);
                lista.AddRange(resultado);
            }
            return lista;
        }

        public async Task<Cargos> ObtenerCargoEmpleado(int id)
        {
            Cargos objeto = new Cargos();
            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseurl);
            var response = await cliente.GetAsync($"api/cargos/{id}");
            if (response.IsSuccessStatusCode)
            {
                var json_repuesta = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<Cargos>(json_repuesta);
                objeto = resultado;
            }
            return objeto;
        }

        public async Task<bool> GuardarCargoEmpleado(Cargos Objeto)
        {
            bool resultado = false;
            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseurl);

            var content = new StringContent(JsonConvert.SerializeObject(Objeto), Encoding.UTF8, "application/json");
            var response = await cliente.PostAsync($"api/cargos", content);

            if (response.IsSuccessStatusCode)
            {
                resultado = true;
            }

            return resultado;
        }

        public async Task<bool> EditarCargoEmpleado(int id, Cargos Objeto)
        {
            bool resultado = false;
            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseurl);

            var content = new StringContent(JsonConvert.SerializeObject(Objeto), Encoding.UTF8, "application/json");
            var response = await cliente.PutAsync($"api/cargos/{id}", content);

            if (response.IsSuccessStatusCode)
            {
                resultado = true;
            }

            return resultado;
        }

        public async Task<bool> EliminarCargoEmpleado(int id)
        {
            bool resultado = false;
            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseurl);

            var response = await cliente.DeleteAsync($"api/cargos/{id}");

            if (response.IsSuccessStatusCode)
            {
                resultado = true;
            }

            return resultado;
        }
        #endregion
    }
}
