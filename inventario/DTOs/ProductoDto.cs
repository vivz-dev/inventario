namespace inventario.DTOs;

public class ProductoDto
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public float Precio { get; set; }
    public string ImagenUrl { get; set; } = string.Empty;
    public int Stock { get; set; } 
    public int UsuarioId { get; set; } 


}