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

namespace Children_Care_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentDetailController : ControllerBase
    {
        private readonly ChildrenCareDbContext _context;
        private readonly IPaymentService _paymentService;

        public PaymentDetailController(ChildrenCareDbContext context, IPaymentService paymentService)
        {
            _paymentService = paymentService;
            _context = context;
        }

        // GET: api/PaymentDetail
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PaymentDetail>>> GetPaymentDetails()
        {
            var paymentDetails = await _paymentService.GetPaymentDetails();
            return Ok(paymentDetails);
        }

        // GET: api/PaymentDetail/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PaymentDetail>> GetPaymentDetail(int id)
        {
            var paymentDetail = await _paymentService.GetPaymentDetailById(id);

            if (paymentDetail == null)
            {
                return NotFound("Payment detail not found");
            }

            return Ok(paymentDetail);
        }

        // PUT: api/PaymentDetail/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPaymentDetail(int id, PaymentDetail paymentDetail)
        {
            try
            {
                var result = await _paymentService.UpdatePaymentDetail(id, paymentDetail);
                return Ok(result);
            }
            catch (ArgumentException)
            {
                return BadRequest("ID mismatch");
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

        // POST: api/PaymentDetail
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PaymentDetail>> PostPaymentDetail(PaymentDetail paymentDetail)
        {
            try
            {
                var result = await _paymentService.AddPaymentDetail(paymentDetail);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // DELETE: api/PaymentDetail/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePaymentDetail(int id)
        {
            try
            {
                var result = await _paymentService.DeletePaymentDetail(id);
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
