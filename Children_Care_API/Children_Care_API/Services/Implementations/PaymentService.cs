using Children_Care_API.DTOs;
using Children_Care_API.Models;
using Children_Care_API.Repositories.Interfaces;
using Children_Care_API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using PaymentAPI.Models;

namespace Children_Care_API.Services.Implementations
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepo;
        public PaymentService (IPaymentRepository paymentRepo)
        {
            _paymentRepo = paymentRepo;   
        }
        public async Task<IEnumerable<Payment>> AddPayment(Payment Payment)
        {
             await _paymentRepo.AddAsync(Payment);
            return await _paymentRepo.GetAllAsync(); ;
        }

        public async Task<IEnumerable<Payment>> DeletePayment(int id)
        {
            var Payment = await _paymentRepo.GetByIdAsync(id);
            if (Payment == null)
            {
                throw new KeyNotFoundException("Payment detail not found");
            }

            await _paymentRepo.DeleteAsync(Payment);

            return await _paymentRepo.GetAllAsync();

        }

        public async Task<Payment> GetPaymentById(int id)
        {
            return await _paymentRepo.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Payment>> GetPayments()
        {
            return await _paymentRepo.GetAllAsync();
        }

        public async Task<IEnumerable<Payment>> UpdatePayment(int id, Payment Payment)
        {
            if (id != Payment.Id)
            {
                throw new ArgumentException("ID mismatch");
            }

            if (!await _paymentRepo.ExistsAsync(id))
            {
                throw new KeyNotFoundException("Payment detail not found");
            }

            _paymentRepo.Update(Payment);
            await _paymentRepo.SaveChangesAsync();

            return await _paymentRepo.GetAllAsync();
        }
    }
}
