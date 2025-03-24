using Children_Care_API.Models;
using Children_Care_API.Repositories.Interfaces;
using Children_Care_API.Services.Interfaces;

namespace Children_Care_API.Services.Implementations
{
	public class UserService : IUserService
	{
		private readonly IUserRepository _userRepository;

		public UserService(IUserRepository userRepository)
		{
			_userRepository = userRepository;
		}

		public async Task<IEnumerable<User>> GetAllUsersAsync() => await _userRepository.GetAllUsersAsync();

		public async Task AddUserAsync(User user) => await _userRepository.AddUserAsync(user);
		public async Task UpdateUserAsync(User user) => await _userRepository.UpdateUserAsync(user);
		public async Task DeleteUserAsync(int id) => await _userRepository.DeleteUserAsync(id);

		public async Task<User> GetUserByIdAsync(int id) => await _userRepository.GetUserByIdAsync(id);
	}
}
