using MinerosERP.Models;

namespace MinerosERP.Services
{
    public interface IServicio_API
    {
        Task<List<Empleados>> Lista();
        Task<object> Obtener(int id);
        Task<bool> Guardar(Empleados EmpObjeto);
        Task<bool> Editar(Empleados EmpObjeto);
        Task<bool> Eliminar(int id);

    }
}
