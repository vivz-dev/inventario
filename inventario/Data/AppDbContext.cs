using Microsoft.EntityFrameworkCore;
using inventario.Models;

namespace inventario.Data;

public class AppDbContext: DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

    public DbSet<Producto> Productos { get; set; }
    public DbSet<PrecioProducto> PreciosProducto { get; set; }
    public DbSet<Usuario> Usuarios { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Relaciones si deseas configurarlas explícitamente (opcional aquí)
        modelBuilder.Entity<Producto>()
            .HasMany(p => p.Precios)
            .WithOne(p => p.Producto)
            .HasForeignKey(p => p.ProductoId);
    }
}