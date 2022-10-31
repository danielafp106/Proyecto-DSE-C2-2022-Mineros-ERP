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

    }
}
