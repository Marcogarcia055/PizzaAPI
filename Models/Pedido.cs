namespace Pizzeria.Models
{
    public class Pedido
    {
        public int IdPedido { get; set; }
        public DateTime Fecha { get; set; }
        public string Cliente { get; set; } = string.Empty;
        public bool Activo { get; set; }
        public decimal Total { get; set; }
        public int? IdCorte { get; set; }

    }
}