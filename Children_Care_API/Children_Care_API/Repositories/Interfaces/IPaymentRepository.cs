using PaymentAPI.Models;

namespace Children_Care_API.Repositories.Interfaces
{
    public interface IPaymentRepository : IBaseRepository<PaymentDetail>
    {
        void Update(PaymentDetail paymentDetail);

        Task<bool> ExistsAsync(int id);

        Task SaveChangesAsync();
    }
}
