namespace Children_Care_API.DTOs
{
	public class ReservationDto
	{
		public int CustomerId { get; set; }
		public int ServiceId { get; set; }
		public string ChildName { get; set; } = string.Empty;
		public string Date { get; set; }  // Dạng chuỗi "yyyy-MM-dd"
		public string Time { get; set; }  // Dạng chuỗi "HH:mm"
	}
}
