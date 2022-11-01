using System.Text.Json.Serialization;

namespace MinerosERP.Models
{
    public class Empleados
    {
        public int id_empleado { get; set; }
        public string nombres_empleado { get; set; }
        public string apellidos_empleado { get; set; }
        public string direccion_empleado { get; set; }
        public string dui_empleado { get; set; }
        public string fecha_nacimiento_empleado { get; set; }
        public string telefono_empleado { get; set; }
        public string numero_isss_empleado { get; set; }
        public string numero_afp_empleado { get; set; }
        public double sueldo_empleado { get; set; }
        public int id_cargo_empleado { get; set; }
        public int id_area { get; set; }
       // public int id_usuario { get; set; }
     //   public string area_nombre { get; set; }
       // public string cargo_nombre { get; set; }
        public long id { get; set; }

    }

}
