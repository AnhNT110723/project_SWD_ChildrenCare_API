using Children_Care_API.Models;
using PaymentAPI.Models;

namespace Children_Care_API.Repositories.Interfaces
{
    public interface IPaymentRepository : IBaseRepository<Payment>
    {
        void Update(Payment Payment);

        Task<bool> ExistsAsync(int id);

        Task SaveChangesAsync();
    }
}
