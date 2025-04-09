using inventario.Data;
using inventario.Models;
using inventario.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace inventario.Services.Implementations;

public class UsuarioService: IUsuarioService
{
    private readonly AppDbContext _context;

    public UsuarioService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Usuario?> ValidarCredenciales(string username, string password)
    {
        return await _context.Usuarios
            .FirstOrDefaultAsync(u => u.Username == username && u.Password == password);
    }

    public async Task<Usuario> Crear(Usuario usuario)
    {
        _context.Usuarios.Add(usuario);
        await _context.SaveChangesAsync();
        return usuario;
    }
}