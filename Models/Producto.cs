namespace Pizzeria.Models
{
    public class Producto
    {
        public int IdProducto { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public decimal Precio { get; set; }
        public bool Activo { get; set; }
        public string? Imagen { get; set; }
        public string? Descripcion { get; set; }   // Nueva propiedad
    }
}