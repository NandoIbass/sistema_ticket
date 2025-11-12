using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HelpDesk.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _cfg;
        public AuthController(IConfiguration cfg) { _cfg = cfg; }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest req)
        {
            var configuredUser = _cfg["Auth:Username"] ?? "admin";
            var configuredPass = _cfg["Auth:Password"] ?? "123456";
            if (req.Username != configuredUser || req.Password != configuredPass) return Unauthorized();

            var claims = new[] { new Claim(ClaimTypes.Name, req.Username) };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_cfg["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: _cfg["Jwt:Issuer"],
                audience: _cfg["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(8),
                signingCredentials: creds
            );
            return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
        }
    }

    public record LoginRequest(string Username, string Password);
}