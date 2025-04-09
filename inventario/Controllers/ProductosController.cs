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
    
    public ProductosController(IProductoService productoService)
    {
        _productoService = productoService;
    }

    [HttpGet]
    public async Task<ActionResult<List<ProductoDto>>> ObtenerTodos()
    {
        var productos = await _productoService.ObtenerTodos();

        var resultado = productos.Select(p => new ProductoDto
        {
            Id = p.Id,
            Nombre = p.Nombre,
            Precios = p.Precios.Select(precio => new PrecioProductoDto
            {
                Id = precio.Id,
                Precio = precio.Precio,
                Lote = precio.Lote,
                Fecha = precio.Fecha
            }).ToList()
        }).ToList();

        return Ok(resultado);
    }

    // GET: api/productos/5
    [HttpGet("{id}")]
    public async Task<ActionResult<ProductoDto>> ObtenerPorId(int id)
    {
        var producto = await _productoService.ObtenerPorId(id);
        if (producto == null) return NotFound();

        var dto = new ProductoDto
        {
            Id = producto.Id,
            Nombre = producto.Nombre,
            Precios = producto.Precios.Select(p => new PrecioProductoDto
            {
                Id = p.Id,
                Precio = p.Precio,
                Lote = p.Lote,
                Fecha = p.Fecha
            }).ToList()
        };

        return Ok(dto);
    }
    
    // POST: api/productos
    [HttpPost]
    public async Task<ActionResult<ProductoDto>> Crear([FromBody] CrearProductoDto dto)
    {
        var producto = new Producto
        {
            Nombre = dto.Nombre,
            Precios = dto.Precios.Select(p => new PrecioProducto
            {
                Precio = p.Precio,
                Lote = p.Lote,
                Fecha = p.Fecha
            }).ToList()
        };

        var creado = await _productoService.Crear(producto);

        var respuesta = new ProductoDto
        {
            Id = creado.Id,
            Nombre = creado.Nombre,
            Precios = creado.Precios.Select(p => new PrecioProductoDto
            {
                Id = p.Id,
                Precio = p.Precio,
                Lote = p.Lote,
                Fecha = p.Fecha
            }).ToList()
        };

        return CreatedAtAction(nameof(ObtenerPorId), new { id = creado.Id }, respuesta);
    }
    
    
    // PUT: api/productos/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Actualizar(int id, [FromBody] ActualizarProductoDto dto)
    {
        if (id != dto.Id) return BadRequest("El ID no coincide.");

        var producto = new Producto
        {
            Id = dto.Id,
            Nombre = dto.Nombre,
            Precios = dto.Precios.Select(p => new PrecioProducto
            {
                Id = p.Id,
                Precio = p.Precio,
                Lote = p.Lote,
                Fecha = p.Fecha,
                ProductoId = dto.Id
            }).ToList()
        };

        var actualizado = await _productoService.Actualizar(producto);
        if (!actualizado) return NotFound();

        return NoContent();
    }

    // DELETE: api/productos/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Eliminar(int id)
    {
        var eliminado = await _productoService.Eliminar(id);
        if (!eliminado) return NotFound();

        return NoContent();
    }
}