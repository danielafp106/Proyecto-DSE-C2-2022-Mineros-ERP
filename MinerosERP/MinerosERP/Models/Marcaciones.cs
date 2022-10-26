namespace MinerosERP.Models
{
    public class Marcaciones
    {
        int IdMarcacion { get; set; }
        DateOnly? FechaMarcacion { get; set; }
        string? HoraEntrada { get; set; }
        string? Salida { get; set; }
        string? TipoMarcacion { get; set; }
        int IdEmpleado { get; set; }
    }
}
