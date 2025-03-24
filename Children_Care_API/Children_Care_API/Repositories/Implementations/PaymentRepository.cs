using Children_Care_API.Data;
using Children_Care_API.Models;
using Children_Care_API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using PaymentAPI.Models;

namespace Children_Care_API.Repositories.Implementations
{
    public class PaymentRepository : BaseRepository<Payment>, IPaymentRepository
    {
        public PaymentRepository(ChildrenCareDbContext context) : base(context) { }

        public void Update(Payment Payment)
        {
            _context.Entry(Payment).State = EntityState.Modified;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Payments.AnyAsync(e => e.Id == id);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
