using Children_Care_API.Models.Enums;

namespace Children_Care_API.DTOs
{
	public class PaymentDto
	{

         public int ReservationId { get; set; }
		public int Id { get; set; }

      
        public decimal Amount { get; set; }

       
        public string PaymentMethod { get; set; }

    
        public string Status { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
       

	}
}
