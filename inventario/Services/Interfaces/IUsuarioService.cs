using inventario.Models;

namespace inventario.Services.Interfaces;

public interface IUsuarioService
{
    Task<Usuario?> ValidarCredenciales(string username, string password);
    Task<Usuario> Crear(Usuario usuario);
}