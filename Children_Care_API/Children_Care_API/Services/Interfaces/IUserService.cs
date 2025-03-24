using Children_Care_API.Models;

namespace Children_Care_API.Services.Interfaces
{
	public interface IUserService
	{
		Task<IEnumerable<User>> GetAllUsersAsync();
		Task<User> GetUserByIdAsync(int id);
		Task AddUserAsync(User user);
		Task UpdateUserAsync(User user);
		Task DeleteUserAsync(int id);
	}
}
