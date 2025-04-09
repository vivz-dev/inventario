namespace inventario.Models;

public class PrecioProducto
{
    public int Id { get; set; }
    public decimal Precio { get; set; }
    public string Lote { get; set; } = string.Empty;
    public DateTime Fecha { get; set; }
        
    public int ProductoId { get; set; }
    public Producto Producto { get; set; } = null!;
}