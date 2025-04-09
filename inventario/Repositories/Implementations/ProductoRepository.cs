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

    public async Task<List<Producto>> ObtenerTodos()
    {
        return await _context.Productos
            .Include(p => p.Precios)
            .ToListAsync();
    }

    public async Task<Producto?> ObtenerPorId(int id)
    {
        return await _context.Productos
            .Include(p => p.Precios)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<Producto> Crear(Producto producto)
    {
        _context.Productos.Add(producto);
        await _context.SaveChangesAsync();
        return producto;
    }

    public async Task<bool> Actualizar(Producto producto)
    {
        if (!_context.Productos.Any(p => p.Id == producto.Id)) return false;
        _context.Productos.Update(producto);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> Eliminar(int id)
    {
        var producto = await ObtenerPorId(id);
        if (producto == null) return false;

        _context.Productos.Remove(producto);
        await _context.SaveChangesAsync();
        return true;
    }
}