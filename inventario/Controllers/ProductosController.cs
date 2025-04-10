using Microsoft.AspNetCore.Mvc;
using inventario.Models;
using inventario.Services.Interfaces;
using inventario.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace inventario.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]

public class ProductosController: ControllerBase
{
    private readonly IProductoService _productoService;
    private readonly IUsuarioService _usuarioService;
    
    public ProductosController(IProductoService productoService, IUsuarioService usuarioService)
    {
        _productoService = productoService;
        _usuarioService = usuarioService;
    }

    [HttpGet]
    public async Task<ActionResult<List<ProductoDto>>> ObtenerTodos()
    {
        var usuario = await ObtenerUsuarioActual();
        if (usuario == null) return Unauthorized();

        var productos = await _productoService.ObtenerPorUsuarioId(usuario.Id);

        var resultado = productos.Select(p => new ProductoDto
        {
            Id = p.Id,
            Nombre = p.Nombre,
            Precio = p.Precio,
            ImagenUrl = p.ImagenUrl,
            Stock = p.Stock,
            UsuarioId = p.UsuarioId
        }).ToList();

        return Ok(resultado);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductoDto>> ObtenerPorId(int id)
    {
        var usuario = await ObtenerUsuarioActual();
        if (usuario == null) return Unauthorized();

        var producto = await _productoService.ObtenerPorId(id);
        if (producto == null || producto.UsuarioId != usuario.Id) return NotFound();

        var dto = new ProductoDto
        {
            Id = producto.Id,
            Nombre = producto.Nombre,
            Precio = producto.Precio,
            ImagenUrl = producto.ImagenUrl,
            Stock = producto.Stock
        };

        return Ok(dto);
    }

    [HttpPost]
    public async Task<ActionResult<ProductoDto>> Crear([FromBody] CrearProductoDto dto)
    {
        var usuario = await ObtenerUsuarioActual();
        if (usuario == null) return Unauthorized();

        var producto = new Producto
        {
            Nombre = dto.Nombre,
            Precio = dto.Precio,
            ImagenUrl = dto.ImagenUrl,
            Stock = dto.Stock,
            UsuarioId = usuario.Id
        };

        var creado = await _productoService.Crear(producto);

        var respuesta = new ProductoDto
        {
            Id = creado.Id,
            Nombre = creado.Nombre,
            Precio = creado.Precio,
            ImagenUrl = creado.ImagenUrl,
            Stock = creado.Stock
        };

        return CreatedAtAction(nameof(ObtenerPorId), new { id = creado.Id }, respuesta);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> Actualizar(int id, [FromBody] ActualizarProductoDto dto)
    {
        if (id != dto.Id) return BadRequest("El ID no coincide.");
        Console.WriteLine((id));
        Console.WriteLine((dto.Id));
        var usuario = await ObtenerUsuarioActual();
        if (usuario == null) return Unauthorized();

        var existente = await _productoService.ObtenerPorId(id);
        if (existente == null || existente.UsuarioId != usuario.Id) return NotFound();

        existente.Nombre = dto.Nombre;
        existente.Precio = dto.Precio;
        existente.ImagenUrl = dto.ImagenUrl;
        existente.Stock = dto.Stock;

        var actualizado = await _productoService.Actualizar(existente);
        if (!actualizado) return NotFound();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Eliminar(int id)
    {
        var usuario = await ObtenerUsuarioActual();
        if (usuario == null) return Unauthorized();

        var producto = await _productoService.ObtenerPorId(id);
        if (producto == null || producto.UsuarioId != usuario.Id) return NotFound();

        var eliminado = await _productoService.Eliminar(id);
        if (!eliminado) return NotFound();

        return NoContent();
    }

    private async Task<Usuario?> ObtenerUsuarioActual()
    {
        var username = User.Identity.Name;
        return await _usuarioService.BuscarPorUsername(username);
    }
}