using Children_Care_API.Data;
using Children_Care_API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using PaymentAPI.Models;

namespace Children_Care_API.Repositories.Implementations
{
    public class PaymentRepository : BaseRepository<PaymentDetail>, IPaymentRepository
    {
        public PaymentRepository(ChildrenCareDbContext context) : base(context) { }

        public void Update(PaymentDetail paymentDetail)
        {
            _context.Entry(paymentDetail).State = EntityState.Modified;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.PaymentDetails.AnyAsync(e => e.PaymentDetailId == id);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
