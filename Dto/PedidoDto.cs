namespace Pizzeria.Dto
{
    public class PedidoDto
    {
        public string Cliente { get; set; } = string.Empty;
        public DateTime Fecha { get; set; } = DateTime.Now;
        public decimal Total { get; set; }
        public List<DetallePedidoDto> Detalles { get; set; } = new List<DetallePedidoDto>();
    }
}