namespace inventario.DTOs;

public class CrearProductoDto
{
    public string Nombre { get; set; } = string.Empty;
    public float Precio { get; set; }
    public string ImagenUrl { get; set; } = string.Empty;
    public int Stock { get; set; }
}