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
        public async Task<IEnumerable<PaymentDetail>> AddPaymentDetail(PaymentDetail paymentDetail)
        {
             await _paymentRepo.AddAsync(paymentDetail);
            return await _paymentRepo.GetAllAsync(); ;
        }

        public async Task<IEnumerable<PaymentDetail>> DeletePaymentDetail(int id)
        {
            var paymentDetail = await _paymentRepo.GetByIdAsync(id);
            if (paymentDetail == null)
            {
                throw new KeyNotFoundException("Payment detail not found");
            }

            await _paymentRepo.DeleteAsync(paymentDetail);

            return await _paymentRepo.GetAllAsync();

        }

        public async Task<PaymentDetail> GetPaymentDetailById(int id)
        {
            return await _paymentRepo.GetByIdAsync(id);
        }

        public async Task<IEnumerable<PaymentDetail>> GetPaymentDetails()
        {
            return await _paymentRepo.GetAllAsync();
        }

        public async Task<IEnumerable<PaymentDetail>> UpdatePaymentDetail(int id, PaymentDetail paymentDetail)
        {
            if (id != paymentDetail.PaymentDetailId)
            {
                throw new ArgumentException("ID mismatch");
            }

            if (!await _paymentRepo.ExistsAsync(id))
            {
                throw new KeyNotFoundException("Payment detail not found");
            }

            _paymentRepo.Update(paymentDetail);
            await _paymentRepo.SaveChangesAsync();

            return await _paymentRepo.GetAllAsync();
        }
    }
}
