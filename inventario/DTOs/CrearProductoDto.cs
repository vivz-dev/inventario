namespace inventario.DTOs;

public class CrearProductoDto
{
    public string Nombre { get; set; } = string.Empty;
    public List<CrearPrecioProductoDto> Precios { get; set; } = new();
}