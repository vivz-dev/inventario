namespace inventario.DTOs;

public class ActualizarProductoDto
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public List<ActualizarPrecioProducto> Precios { get; set; } = new();
}