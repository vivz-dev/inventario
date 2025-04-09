namespace inventario.DTOs;

public class ActualizarPrecioProducto
{
    public int Id { get; set; }
    public decimal Precio { get; set; }
    public string Lote { get; set; } = string.Empty;
    public DateTime Fecha { get; set; }
}