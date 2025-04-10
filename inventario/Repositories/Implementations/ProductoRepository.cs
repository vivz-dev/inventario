using inventario.Data;
using inventario.Models;
using inventario.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace inventario.Repositories.Implementations;

public class ProductoRepository: IProductoRepository
{
    private readonly AppDbContext _context;

    public ProductoRepository(AppDbContext context)
    {
        _context = context;
    }

    // Obtener todos los productos de un usuario
    public async Task<List<Producto>> ObtenerTodos(int usuarioId)
    {
        return await _context.Productos
            .Where(p => p.UsuarioId == usuarioId)  // Filtra productos por usuario
            .Include(p => p.Precio) // Asegúrate de incluir la lista de precios
            .ToListAsync();
    }

    // Obtener un producto específico por Id
    public async Task<Producto?> ObtenerPorId(int id)
    {
        return await _context.Productos
            .Include(p => p.Precio)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    // Crear un nuevo producto asociado al usuario
    public async Task<Producto> Crear(Producto producto)
    {
        _context.Productos.Add(producto); 
        await _context.SaveChangesAsync();
        return producto;
    }

    public async Task<bool> Actualizar(Producto producto)
    {
        var existente = await _context.Productos.FirstOrDefaultAsync(p => p.Id == producto.Id);

        if (existente == null || existente.UsuarioId != producto.UsuarioId) return false;

        _context.Entry(existente).CurrentValues.SetValues(producto);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> Eliminar(int id)
    {
        var producto = await ObtenerPorId(id);
        
        // Verificar que el producto pertenece al usuario
        if (producto == null) return false;

        _context.Productos.Remove(producto);
        await _context.SaveChangesAsync();
        return true;
    }
    
    public async Task<List<Producto>> ObtenerPorUsuarioId(int usuarioId)
    {
        return await _context.Productos
            .Where(p => p.UsuarioId == usuarioId) // Filtra por el ID del usuario
            .Include(p => p.Precio)
            .ToListAsync();
    }


}