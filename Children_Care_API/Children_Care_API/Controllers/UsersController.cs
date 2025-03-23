using Children_Care_API.DTOs;
using Children_Care_API.Helpers;
using Children_Care_API.Models;
using Children_Care_API.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Children_Care_API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UsersController : Controller
	{
			private readonly IConfiguration _configuration;
			private readonly IUserRepository _userRepository; // Dịch vụ kết nối với cơ sở dữ liệu
			private readonly IJwtHelper _jwtHelper;

			public UsersController(IConfiguration configuration, IUserRepository userRepository, IJwtHelper jwtHelper)
			{
				_configuration = configuration;
				_userRepository = userRepository;
				_jwtHelper = jwtHelper;
			}

			[HttpPost]
			public async Task<IActionResult> Login([FromBody] LoginDto loginDTO)
			{
				User user = await _userRepository.GetUserByEmailAndPasswordAsync(loginDTO);

				if (user != null)
				{
					var token = _jwtHelper.GenerateToken(user);
					return Ok(new { token = token, message = "Login successful" });
				}
				return Unauthorized(new { message = "Invalid credentials" });
			}
			[HttpPost("logout")]
			public IActionResult Logout()
			{
				HttpContext.Session.Clear();
				HttpContext.Session.Remove("Email");
				return RedirectToAction("Login");
			}

		}
	}

