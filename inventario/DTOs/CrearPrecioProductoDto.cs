namespace inventario.DTOs;

public class CrearPrecioProductoDto
{
    public decimal Precio { get; set; }
    public string Lote { get; set; } = string.Empty;
    public DateTime Fecha { get; set; }
}