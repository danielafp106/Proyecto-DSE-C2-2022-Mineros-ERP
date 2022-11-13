using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using MinerosERP.Models;
using System;

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

        #region LOGIN

        public async Task<LoginResponse> Login(Login Objeto)
        {
            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseurl);
            LoginResponse user = new LoginResponse();

            var content = new StringContent(JsonConvert.SerializeObject(Objeto), Encoding.UTF8, "application/json");

            var response = await cliente.PostAsync($"api/authentication/login/", content);


            if (response.IsSuccessStatusCode)
            {
                var json_repuesta = await response.Content.ReadAsStringAsync();
                user = JsonConvert.DeserializeObject<LoginResponse>(json_repuesta);
                user.username = Objeto.username;
                var response2 = await cliente.GetAsync($"api/authentication/user/?username={user.username}");
                if (response2.IsSuccessStatusCode)
                {
                    var json_repuesta2 = await response2.Content.ReadAsStringAsync();
                    var userInformation = JsonConvert.DeserializeObject<LoginResponse>(json_repuesta2);
                    user.pk = userInformation.pk;
                    user.first_name = userInformation.first_name;
                    user.last_name = userInformation.last_name;
                }
                else
                {
                    return new LoginResponse();
                }
                return user;
            }
            //Aqui se debe de enviar el mensaje de error que response la api
            return user;
        }
        #endregion

        #region REGISTRATION
        public async Task<string> Register(Registration obj)
        {
            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseurl);

            var content = new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");

            var response = await cliente.PostAsync($"api/registration", content);


            if (response.IsSuccessStatusCode)
            {
                //string a = response.ToString();
                //Console.WriteLine(a);
                return "true";
            }
            //Aqui se debe de enviar el mensaje de error que response la api
            return "false";
        }
        #endregion

        #region USUARIOS
        public async Task<List<Usuarios>> ListarUsuarios()
        {
            List<Usuarios> lista = new List<Usuarios>();
            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseurl);
            var response = await cliente.GetAsync("api/usuarios");
            if (response.IsSuccessStatusCode)
            {
                var json_repuesta = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<List<Usuarios>>(json_repuesta);
                lista.AddRange(resultado);
            }
            return lista;
        }

        public async Task<Usuarios> ObtenerUsuario(int id)
        {
            Usuarios objeto = new Usuarios();
            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseurl);
            var response = await cliente.GetAsync($"api/usuarios/{id}");
            if (response.IsSuccessStatusCode)
            {
                var json_repuesta = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<Usuarios>(json_repuesta);
                objeto = resultado;
            }
            return objeto;
        }
        #endregion
    }
}
