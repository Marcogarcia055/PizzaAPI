namespace Pizzeria.Models
{
    public class Corte
    {
        public int IdCorte { get; set; }
        public DateTime FechaCorte { get; set; }
        public int TotalPedidos { get; set; }
        public decimal TotalVentas { get; set; }
        public string? Observaciones { get; set; }

        // Relación con pedidos
        public List<Pedido> Pedidos { get; set; } = new List<Pedido>();
    }
}