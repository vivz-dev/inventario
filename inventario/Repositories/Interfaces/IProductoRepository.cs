using inventario.Models;

namespace inventario.Repositories.Interfaces;

public interface IProductoRepository
{
    Task<List<Producto>> ObtenerTodos(int usuarioId);
    Task<Producto?> ObtenerPorId(int id);
    Task<Producto> Crear(Producto producto);
    Task<bool> Actualizar(Producto producto);
    Task<bool> Eliminar(int id);
}