using Children_Care_API.Data;
using Children_Care_API.Helpers;
using Children_Care_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.EntityFrameworkCore;
using System;

namespace Children_Care_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly ChildrenCareDbContext _context;

        public AdminController(ChildrenCareDbContext context)
        {
            _context = context;
        }
        [HttpGet("users")]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _context.Users.ToListAsync();
            return Ok(users);
        }
        [HttpGet("users/{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return NotFound();
            return Ok(user);
        }
        [HttpPost("users")]
        public async Task<IActionResult> CreateUser([FromBody] User user)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            user.PasswordHash = Sha256HashHelper.ComputeSha256Hash(user.PasswordHash);
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
        }
        [HttpPut("users/{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] User userUpdate)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return NotFound();

            user.FullName = userUpdate.FullName;
            user.Email = userUpdate.Email;
            user.Role = userUpdate.Role;

            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return Ok(user);
        }
        [HttpDelete("users/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return NotFound();

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        [HttpGet("stats")]
        public async Task<IActionResult> GetStats()
        {
            var userCount = await _context.Users.CountAsync();
            var serviceCount = await _context.Services.CountAsync();
            var feedbackCount = await _context.Feedbacks.CountAsync();

            return Ok(new { users = userCount, services = serviceCount, feedbacks = feedbackCount });
        }
    }
}
