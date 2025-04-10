namespace inventario.Models
{
    public class Producto
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; } 
        public Usuario Usuario { get; set; } = null!;
        public string Nombre { get; set; } = string.Empty;
        public float Precio { get; set; }
        public string ImagenUrl { get; set; } = string.Empty; 
        public int Stock { get; set; }
    }
}