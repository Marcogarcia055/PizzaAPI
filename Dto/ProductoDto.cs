namespace Pizzeria.Dto
{
    public class ProductoDto
    {
        public string Nombre { get; set; } = string.Empty;
        public decimal Precio { get; set; }
        public string? Imagen { get; set; }
        public string? Descripcion { get; set; }
        public string? Categoria { get; set; }   // 🔹 Nueva propiedad
    }
}