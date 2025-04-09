namespace inventario.Models
{
    public class Producto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public ICollection<PrecioProducto> Precios { get; set; } = new List<PrecioProducto>();
    }
}