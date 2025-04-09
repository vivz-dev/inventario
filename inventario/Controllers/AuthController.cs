using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using inventario.Config;
using inventario.Models;
using inventario.DTOs;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using inventario.Services.Interfaces;

namespace inventario.Controllers;

[ApiController]
[Route("api/[controller]")]

public class AuthController : ControllerBase
{
    private readonly JwtConfig _jwt;
    private readonly IUsuarioService _usuarioService;

    public AuthController(IOptions<JwtConfig> jwt, IUsuarioService usuarioService)
    {
        _jwt = jwt.Value;
        _usuarioService = usuarioService;

    }
    
    [HttpPost("login")]
    public async Task<ActionResult<LoginResponseDto>> Login([FromBody] LoginRequestDto dto)
    {
        var usuario = await _usuarioService.ValidarCredenciales(dto.Username, dto.Password);

        if (usuario == null)
            return Unauthorized();

        var claims = new[] {
            new Claim(ClaimTypes.Name, usuario.Username)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _jwt.Issuer,
            audience: _jwt.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: creds
        );

        return Ok(new LoginResponseDto
        {
            Token = new JwtSecurityTokenHandler().WriteToken(token)
        });
    }

    [HttpPost("registro")]
    public async Task<ActionResult> Registrar([FromBody] LoginRequestDto dto)
    {
        var usuario = new Usuario
        {
            Username = dto.Username,
            Password = dto.Password // En producción: encripta esto
        };

        await _usuarioService.Crear(usuario);
        return Ok("Usuario registrado correctamente");
    }

    
}