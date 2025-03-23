using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Children_Care_API.Data;
using PaymentAPI.Models;
using Children_Care_API.Services.Interfaces;
using Children_Care_API.Models;
using Children_Care_API.DTOs;
using Microsoft.AspNetCore.Authentication.BearerToken;
using Children_Care_API.Models.Enums;

namespace Children_Care_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly ChildrenCareDbContext _context;
        private readonly IPaymentService _paymentService;

        public PaymentController(ChildrenCareDbContext context, IPaymentService paymentService)
        {
            _paymentService = paymentService;
            _context = context;
        }

        // GET: api/Payment
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PaymentDto>>> GetPayments()
        {
            var Payments = await _context.Payments.Select(x => new PaymentDto
            {
                Id = x.Id,
                Amount = x.Amount,
                PaymentMethod = x.PaymentMethod.ToString(),
                Status = x.Status.ToString(),
                CreatedAt = x.CreatedAt

            }).ToListAsync();

            return Ok(Payments);
        }

        // GET: api/Payment/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Payment>> GetPayment(int id)
        {
            var Payment = await _paymentService.GetPaymentById(id);

            if (Payment == null)
            {
                return NotFound("Payment detail not found");
            }

            return Ok(Payment);
        }

        // PUT: api/Payment/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPayment(int id, PaymentDto paymentDto)
        {
            var payment = await _context.Payments.FindAsync(id);
            if (payment == null)
            {
                return NotFound();
            }
            payment.Amount = paymentDto.Amount;
            payment.PaymentMethod = Enum.Parse<PaymentMethod>(paymentDto.PaymentMethod);
             payment.Status = Enum.Parse<PaymentStatus>(paymentDto.Status);
              
            await _context.SaveChangesAsync();
            return Ok();
        }

        // POST: api/Payment
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Payment>> PostPayment(PaymentDto paymentDto)
        {
            if (paymentDto == null)
            {
                return BadRequest();
            }
            var payment = new Payment
            {
                ReservationId = 3,
                Amount = paymentDto.Amount,
                PaymentMethod = Enum.Parse<PaymentMethod>(paymentDto.PaymentMethod),
                Status = Enum.Parse<PaymentStatus>(paymentDto.Status),
            };
            _context.Payments.Add(payment);
            await _context.SaveChangesAsync();
            return Ok();
        }

        // DELETE: api/Payment/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePayment(int id)
        {
            try
            {
                var result = await _paymentService.DeletePayment(id);
                return Ok(result);
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Payment detail not found");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

        }
    }
}
