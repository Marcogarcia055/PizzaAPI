namespace Pizzeria.Models
{
    public class CorteGetAllDto
    {
        public int IdCorte { get; set; }
        public DateTime FechaCorte { get; set; }
        public int TotalPedidos { get; set; }
        public decimal TotalVentas { get; set; }
        public string? Observaciones { get; set; }
    }
}