namespace MinerosERP.Models
{
    public class Marcaciones
    {
        public int id_marcacion { get; set; }
        public string fecha_marcacion { get; set; }
        public string hora_entrada { get; set; }
        public string hora_salida { get; set; }
        public string tipo_marcacion { get; set; }
        public int id_empleado { get; set; }
        public string? total_horas { get; set; }
    }
}
