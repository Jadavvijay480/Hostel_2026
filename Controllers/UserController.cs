using Hostel_2026.Data;
using Hostel_2026.Models;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly UserRepository _userRepository;

    public UserController(IConfiguration configuration)
    {
        _userRepository = new UserRepository(configuration);
    }


    // LOGIN
    [HttpPost("Login")]
    public IActionResult Login(LoginModel login)
    {
        var result = _userRepository.Login(login.Email, login.Password);

        if (result == null)
        {
            return Unauthorized(new
            {
                success = false,
                message = "Invalid Email or Password or Inactive User"
            });
        }

        return Ok(new
        {
            success = true,
            message = "User successfully logged in",
            data = result
        });
    }

    // SIGNUP
    [HttpPost("Register")]
    public IActionResult Register(UserModel user)
    {
        var result = _userRepository.Register(user);

        if (result)
        {
            return Ok("Registration Successful");
        }

        return BadRequest("Registration Failed");
    }

}