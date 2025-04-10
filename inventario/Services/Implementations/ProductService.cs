using inventario.Data;
using inventario.Models;
using inventario.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace inventario.Services.Implementations;

public class ProductoService : IProductoService
{
    private readonly AppDbContext _context;

    public ProductoService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Producto>> ObtenerTodos()
    {
        return await _context.Productos.ToListAsync();
    }

    public async Task<Producto> ObtenerPorId(int id)
    {
        var producto =  await _context.Productos.FindAsync(id);
        return producto ?? new Producto();
    }

    public async Task<List<Producto>> ObtenerPorUsuarioId(int usuarioId)
    {
        return await _context.Productos
            .Where(p => p.UsuarioId == usuarioId) 
            .ToListAsync(); 
    }

    public async Task<Producto> Crear(Producto producto)
    {
        _context.Productos.Add(producto);
        await _context.SaveChangesAsync();
        return producto;
    }

    public async Task<bool> Actualizar(Producto producto)
    {
        var existente = await _context.Productos.FindAsync(producto.Id);
        if (existente == null) return false;

        existente.Nombre = producto.Nombre;
        existente.Precio = producto.Precio;
        existente.ImagenUrl = producto.ImagenUrl;
        existente.Stock = producto.Stock;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> Eliminar(int id)
    {
        var producto = await _context.Productos.FindAsync(id);
        if (producto == null) return false;

        _context.Productos.Remove(producto);
        await _context.SaveChangesAsync();
        return true;
    }}