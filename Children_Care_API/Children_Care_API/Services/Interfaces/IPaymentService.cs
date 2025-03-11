using Microsoft.AspNetCore.Mvc;
using PaymentAPI.Models;

namespace Children_Care_API.Services.Interfaces
{
    public interface IPaymentService
    {
        Task<IEnumerable<PaymentDetail>> GetPaymentDetails();

        Task<PaymentDetail> GetPaymentDetailById(int id);

        Task<IEnumerable<PaymentDetail>> UpdatePaymentDetail(int id, PaymentDetail paymentDetail);

        Task<IEnumerable<PaymentDetail>> AddPaymentDetail(PaymentDetail paymentDetail);

        Task<IEnumerable<PaymentDetail>> DeletePaymentDetail(int id);

    }
}
