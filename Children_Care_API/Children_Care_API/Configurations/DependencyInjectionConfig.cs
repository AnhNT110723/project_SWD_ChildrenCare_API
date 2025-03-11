using Children_Care_API.Repositories.Implementations;
using Children_Care_API.Repositories.Interfaces;
using Children_Care_API.Services.Implementations;
using Children_Care_API.Services.Interfaces;

namespace Children_Care_API.Configurations
{
    public static class DependencyInjectionConfig
    {
        public static void AddDependencyInjectionConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IPaymentRepository, PaymentRepository>();
            services.AddScoped<IPaymentService, PaymentService>();
        }
    }
}
