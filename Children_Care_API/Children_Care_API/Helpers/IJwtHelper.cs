
using Children_Care_API.Models;

namespace Children_Care_API.Helpers
{
    public interface IJwtHelper
    {
        string GenerateToken(User user);
    }

}
