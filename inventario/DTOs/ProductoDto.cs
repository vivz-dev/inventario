namespace inventario.DTOs;

public class ProductoDto
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public List<PrecioProductoDto> Precios { get; set; } = new();

}