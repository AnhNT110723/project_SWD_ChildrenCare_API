using Children_Care_API.Models.Enums;

namespace Children_Care_API.DTOs.Reservations
{
    public class ReservationDto :BaseReservationDto
    {
        public int Id { get; set; }
        public string ServiceName { get; set; }
        public string CustomerName { get; set; }
        public DateTime ReservationDate { get; set; }
        public DateTime CreateAt { get; set; }
        public string Status { get; set; }
        public decimal Amount { get; set; }


    }
}
