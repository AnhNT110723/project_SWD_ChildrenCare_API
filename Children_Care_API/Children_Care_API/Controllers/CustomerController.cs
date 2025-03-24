using Children_Care_API.DTOs;
using Children_Care_API.Helpers;
using Children_Care_API.Models;
using Children_Care_API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Children_Care_API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CustomerController : Controller
	{
		private readonly IUserService _userService;

		public CustomerController(IUserService userService)
		{
			_userService = userService;
		}

		//// [Authorize(Roles = "Manager, Admin")]
		[HttpPost]
		public async Task<IActionResult> AddCustomer([FromBody] UserDto newUserDto)
		{
			if (newUserDto == null)
			{
				return BadRequest("Invalid employee data.");
			}

			var hashedPassword = Sha256HashHelper.ComputeSha256Hash(newUserDto.PasswordHash);

			var newUser = new User
			{
				FullName = newUserDto.FullName,
				Email = newUserDto.Email,
				PasswordHash = hashedPassword,
				CreatedAt = newUserDto.CreatedAt,
				Role = newUserDto.Role
			};

			try
			{
				await _userService.AddUserAsync(newUser);
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				return StatusCode(500, "An error occurred while adding the employee.");
			}

			return CreatedAtAction(nameof(GetAllCustomer), new { id = newUser.Id }, newUser);
		}

		[HttpGet]
		public async Task<IActionResult> GetAllCustomer()
		{
				var users = await _userService.GetAllUsersAsync();
				return Ok(users);
			
		}

		[Authorize(Roles = "Admin")]
		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateCustomer(int id, [FromBody] UserDto updatedUserDto)
		{
			// Check for ID mismatch
			if (id != updatedUserDto.Id)
			{
				return BadRequest("Employee ID mismatch");
			}

			// Retrieve user from service
			var user = await _userService.GetUserByIdAsync(id);
			if (user == null)
			{
				return NotFound("User not found");
			}

			// Update user properties
			user.FullName = updatedUserDto.FullName ;
			user.Email = updatedUserDto.Email;
			user.PasswordHash = Sha256HashHelper.ComputeSha256Hash(updatedUserDto.PasswordHash);
			user.CreatedAt = updatedUserDto.CreatedAt;
			user.Role = updatedUserDto.Role;

			try
			{
				// Attempt to update the user in the service
				await _userService.UpdateUserAsync(user);
			}
			catch (Exception e)
			{
				// Log the exception (optional)
				Console.WriteLine(e);
				// Return a 500 Internal Server Error with a consistent message
				return StatusCode(500, "An error occurred while updating the employee.");
			}

			// Return NoContent if the update is successful
			return NoContent();
		}

		[Authorize(Roles = "Manager, Admin")]
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteCustomer(int id)
		{
			var user = await _userService.GetUserByIdAsync(id);
			if (user == null)
			{
				return NotFound($"Employee with ID {id} not found.");
			}

			try
			{
				await _userService.DeleteUserAsync(id);
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				return StatusCode(500, "An error occurred while deleting the employee.");
			}

			return NoContent(); // Trả về NoContent (204) sau khi xóa thành công
		}
	}
}
