using Children_Care_API.Data;
using Microsoft.EntityFrameworkCore;

namespace Children_Care_API.Configurations
{
    public static class DbContextConfig
    {
        public static void AddDbContextConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ChildrenCareDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("MyDB")));

        }
    }
}
