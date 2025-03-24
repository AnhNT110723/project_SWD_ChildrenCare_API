﻿using Children_Care_API.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace Children_Care_API.DTOs
{
	public class UserDto
	{
		public int Id { get; set; }
		public string FullName { get; set; } = string.Empty;
		public string Email { get; set; } = string.Empty;
		public string PasswordHash { get; set; } = string.Empty;
		public Role Role { get; set; }

		public DateTime CreatedAt { get; set; }
	}
}
