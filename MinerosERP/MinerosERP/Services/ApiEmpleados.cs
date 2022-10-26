using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using MinerosERP.Models;

namespace MinerosERP.Services
{
    public class ApiEmpleados : IServicio_API
    {
        private static string _baseurl;

        public ApiEmpleados()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            _baseurl = builder.GetSection("ApiSettings:baseUrl").Value;

        }

        public async Task<List<Empleados>> Lista()
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

        public Task<bool> Editar(Empleados EmpObjeto)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Eliminar(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Guardar(Empleados EmpObjeto)
        {
            throw new NotImplementedException();
        }

        public Task<object> Obtener(int id)
        {
            throw new NotImplementedException();
        }
    }
}
