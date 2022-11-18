using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace MinerosERP.Models
{
    public class Empleados
    {
        public int id_empleado { get; set; }

        [Required(ErrorMessage = "Campo obligatorio.")]
        public string nombres_empleado { get; set; }

        [Required(ErrorMessage = "Campo obligatorio.")]
        public string apellidos_empleado { get; set; }

        [Required(ErrorMessage = "Campo obligatorio.")]
        public string direccion_empleado { get; set; }

        [Required(ErrorMessage = "Campo obligatorio.")]
        [StringLength(9, MinimumLength = 9, ErrorMessage = "Favor digitar DUI con 8 digitos y el guión")]
        public string dui_empleado { get; set; }

        [Required(ErrorMessage = "Campo obligatorio.")]
        public string fecha_nacimiento_empleado { get; set; }

        [Required(ErrorMessage = "Campo obligatorio.")]
        [StringLength(9, MinimumLength = 8, ErrorMessage = "Ingresar un télefono válido")]
        public string telefono_empleado { get; set; }

        [StringLength(10, MinimumLength = 10, ErrorMessage = "Ingresar número de ISSS válido, con longitud de 10 caracteres")]
        public string numero_isss_empleado { get; set; }
        [Required(ErrorMessage = "Campo obligatorio.")]
        [StringLength(12, MinimumLength = 12, ErrorMessage = "Ingresar número de AFP válido, con longitud de 12 caracteres")]
        public string numero_afp_empleado { get; set; }

        [Required(ErrorMessage = "Campo obligatorio.")]
        public double sueldo_empleado { get; set; }

        [Required(ErrorMessage = "Campo obligatorio.")]
        public int id_cargo_empleado { get; set; }

        [Required(ErrorMessage = "Campo obligatorio.")]
        public int id_area { get; set; }

        public int id_usuario { get; set; }
        [Required(ErrorMessage = "Campo obligatorio.")]
        public string? usuario { get; set; }
        public string? cargo_nombre { get; set; }

        public string? area { get; set; }
        [Required(ErrorMessage = "Campo obligatorio.")]
        public string? passwordtemp { get; set; }

       
        [Required(ErrorMessage = "Campo obligatorio.")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string? email { get; set; }

    }

}
