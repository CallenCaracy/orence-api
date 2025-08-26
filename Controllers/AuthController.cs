using Microsoft.AspNetCore.Mvc;

namespace JlabsBackend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    [HttpPost("register")]
    public IActionResult Register([FromBody] UserDto user)
    {
        // For now, just echo the user info
        Console.WriteLine(user.Name);
        return Ok(new { message = "User registered!", user });
    }
}

// DTO class for user registration
public class UserDto
{
    public string Name { get; set; } = "";
    public string Email { get; set; } = "";
    public string Password { get; set; } = "";
}
