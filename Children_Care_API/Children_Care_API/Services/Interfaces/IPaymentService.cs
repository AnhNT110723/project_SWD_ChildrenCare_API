using Children_Care_API.Models;
using Microsoft.AspNetCore.Mvc;
using PaymentAPI.Models;

namespace Children_Care_API.Services.Interfaces
{
    public interface IPaymentService
    {
        Task<IEnumerable<Payment>> GetPayments();

        Task<Payment> GetPaymentById(int id);

        Task<IEnumerable<Payment>> UpdatePayment(int id, Payment Payment);

        Task<IEnumerable<Payment>> AddPayment(Payment Payment);

        Task<IEnumerable<Payment>> DeletePayment(int id);

    }
}
