﻿using Children_Care_API.Data;
using Children_Care_API.DTOs;
using Children_Care_API.Helpers;
using Children_Care_API.Models;
using Children_Care_API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Children_Care_API.Repositories.Implementations
{
	public class UserRepository : IUserRepository
	{
		private readonly ChildrenCareDbContext _context;

		public UserRepository(ChildrenCareDbContext context)
		{
			_context = context;
		}

		public async Task<IEnumerable<User>> GetAllUsersAsync() => await _context.Users.ToListAsync();
		public async Task AddUserAsync(User user)
		{
			await _context.Users.AddAsync(user);
			await _context.SaveChangesAsync();
		}
		public async Task UpdateUserAsync(User user)
		{
			_context.Users.Update(user);
			await _context.SaveChangesAsync();
		}
		public async Task DeleteUserAsync(int id)
		{
			var user = await _context.Users.FindAsync(id);
			if (user != null)
			{
				_context.Users.Remove(user);
				await _context.SaveChangesAsync();
			}
		}

		public async Task<User> GetUserByEmailAndPasswordAsync(LoginDto loginDTO)
		{
			var user = await _context.Users
				.Include(x => x.Role)
				.FirstOrDefaultAsync(u => u.Email == loginDTO.Email);
			if (user != null)
			{
				if (user.PasswordHash == Sha256HashHelper.ComputeSha256Hash(loginDTO.PasswordHash))
				{
					return user;
				}
			}
			return null;
		}

		public async Task<User> GetUserByIdAsync(int id)
		{
			return await _context.Users
				.FirstOrDefaultAsync(x => x.Id == id);
		}
	}
}

