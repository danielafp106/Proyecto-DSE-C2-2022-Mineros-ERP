using MinerosERP.Models;

namespace MinerosERP.Services
{
    public interface IServicio_API
    {
        //EMPLEADOS
        Task<List<Empleados>> ListarEmpleados();
        Task<Empleados> ObtenerEmpleado(int id);
        Task<bool> GuardarEmpleado(Empleados Objeto);
        Task<bool> EditarEmpleado(int id, Empleados Objeto);
        Task<bool> EliminarEmpleado(int id);
        
        //MARCACIONES
        Task<List<Marcaciones>> ListarMarcaciones();
        Task<Marcaciones> ObtenerMarcacion(int id);
        Task<bool> GuardarMarcacion(Marcaciones Objeto);
        Task<bool> EditarMarcacion(int id, Marcaciones Objeto);
        Task<bool> EliminarMarcacion(int id);
        
        //AREAS
        Task<List<Areas>> ListarAreas();
        Task<Areas> ObtenerArea(int id);
        Task<bool> GuardarArea(Areas Objeto);
        Task<bool> EditarArea(int id, Areas Objeto);
        Task<bool> EliminarArea(int id);

        //CARGOS EMPLEADOS
        Task<List<Cargos>> ListarCargosEmpleados();
        Task<Cargos> ObtenerCargoEmpleado(int id);
        Task<bool> GuardarCargoEmpleado(Cargos Objeto);
        Task<bool> EditarCargoEmpleado(int id, Cargos Objeto);
        Task<bool> EliminarCargoEmpleado(int id);

        //LOGIN 
        Task<LoginResponse> Login(Login Objeto);

        //REGISTRATION
        Task<Registration> Register(Registration Objeto);

        //USUARIOS
        Task<List<Usuarios>> ListarUsuarios();
        Task<Usuarios> ObtenerUsuario(int id);

    }
}
