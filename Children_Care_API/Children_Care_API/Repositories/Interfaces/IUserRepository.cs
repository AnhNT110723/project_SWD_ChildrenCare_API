using Children_Care_API.DTOs;
using Children_Care_API.Models;

namespace Children_Care_API.Repositories.Interfaces
{
	public interface IUserRepository
	{
		
		Task<IEnumerable<User>> GetAllUsersAsync();
		Task<User> GetUserByIdAsync(int id);
		Task AddUserAsync(User user);
		Task UpdateUserAsync(User user);
		Task DeleteUserAsync(int id);
		Task<User> GetUserByEmailAndPasswordAsync(LoginDto loginDTO);
	}
}
