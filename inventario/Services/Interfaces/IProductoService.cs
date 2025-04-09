using inventario.Models;

namespace inventario.Services.Interfaces;

public interface IProductoService
{
    Task<List<Producto>> ObtenerTodos();
    Task<Producto?> ObtenerPorId(int id);
    Task<Producto> Crear(Producto producto);
    Task<bool> Actualizar(Producto producto);
    Task<bool> Eliminar(int id);
}