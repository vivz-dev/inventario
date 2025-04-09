using inventario.Models;
using inventario.Repositories.Interfaces;
using inventario.Services.Interfaces;

namespace inventario.Services.Implementations;

public class ProductoService : IProductoService
{
    private readonly IProductoRepository _repositorio;

    public ProductoService(IProductoRepository repositorio)
    {
        _repositorio = repositorio;
    }

    public Task<List<Producto>> ObtenerTodos() => _repositorio.ObtenerTodos();
    public Task<Producto?> ObtenerPorId(int id) => _repositorio.ObtenerPorId(id);
    public Task<Producto> Crear(Producto producto) => _repositorio.Crear(producto);
    public Task<bool> Actualizar(Producto producto) => _repositorio.Actualizar(producto);
    public Task<bool> Eliminar(int id) => _repositorio.Eliminar(id);
}