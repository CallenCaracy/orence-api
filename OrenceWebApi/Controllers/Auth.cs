using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrenceApi.Data;
using OrenceApi.Models;

namespace OrenceApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly JwtService _jwtService;

        public AuthController(AppDbContext context, JwtService jwtService)
        {
            _context = context;
            _jwtService = jwtService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserDto user)
        {
            if (await _context.Users.AnyAsync(u => u.email == user.Email))
                return Conflict(new { message = "Email already registered" });

            var newUser = new User
            {
                name = user.Name,
                email = user.Email,
                password_hash = BCrypt.Net.BCrypt.HashPassword(user.Password)
            };

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            return Ok(new { message = "User registered!", user = new { newUser.id, newUser.name, newUser.email } });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto login)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.email == login.Email);
            if (user == null) return Unauthorized(new { message = "User not found" });

            if (!BCrypt.Net.BCrypt.Verify(login.Password, user.password_hash))
                return Unauthorized(new { message = "Invalid credentials" });

            var token = _jwtService.GenerateToken(user);
            return Ok(new { message = "Login successful", token, user = new { user.id, user.name, user.email } });
        }
    }
}
