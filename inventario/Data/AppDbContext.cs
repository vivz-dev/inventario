using Microsoft.EntityFrameworkCore;
using inventario.Models;

namespace inventario.Data;

public class AppDbContext: DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

    public DbSet<Producto> Productos { get; set; }
    public DbSet<Usuario> Usuarios { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Relaciones si deseas configurarlas explícitamente (opcional aquí)
        modelBuilder.Entity<Producto>()
            .HasOne(p => p.Usuario)
            .WithMany(u => u.Productos)
            .HasForeignKey(p => p.UsuarioId)
            .OnDelete(DeleteBehavior.Cascade); 
    }
}