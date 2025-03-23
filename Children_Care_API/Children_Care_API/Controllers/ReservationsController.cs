using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Children_Care_API.Data;
using Children_Care_API.Models;
using Children_Care_API.Models.Enums;
using System.Text.Json.Serialization;
using System.Text.Json;
using Children_Care_API.DTOs.Reservations;

namespace Children_Care_API.Controllers
{
    [Route("api/[controller]")]
	[ApiController]
	public class ReservationsController : ControllerBase
	{
		private readonly ChildrenCareDbContext _context;

		public ReservationsController(ChildrenCareDbContext context)
		{
			_context = context;
		}

		// GET: api/Reservations
		[HttpGet]
		public async Task<ActionResult<IEnumerable<Reservation>>> GetReservations()
		{
			var reservations = await _context.Reservations
				.Include(r => r.Service)
				.Include(r => r.Customer)
				.Select(r => new ReservationDto
				{
					 Id = r.Id,
					ServiceName = r.Service.Name,
					CustomerName = r.Customer.FullName,
					ChildName = r.ChildName,
					CreateAt = r.CreatedAt,
					ReservationDate = r.ReservationDate,
					Status = r.Status.ToString(),
				})
			.ToListAsync();

			return Ok(reservations);
		}

		// GET: api/Reservations/5
		[HttpGet("{id}")]
		public async Task<ActionResult<Reservation>> GetReservation(int id)
		{
			var reservation = await _context.Reservations.FindAsync(id);

			if (reservation == null)
			{
				return NotFound();
			}

			return reservation;
		}

		// PUT: api/Reservations/5
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPut("{id}")]
		public async Task<IActionResult> PutReservation(int id, ReservationUpdateDto reservationDto)
		{
			var reservation = await _context.Reservations.FindAsync(id);
			if (reservation == null)
			{
				return NotFound();
			}

			reservation.ChildName = reservationDto.ChildName;
			reservation.ReservationDate = DateTime.Parse($"{reservationDto.Date} {reservationDto.Time}");
			reservation.ServiceId = reservationDto.ServiceId;
			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
					throw;
			}

			return Ok(new
			{
				message = "Cập nhật thành công!",
				updatedReservation = new
				{
					reservation.Id,
					reservation.ChildName,
					reservation.ReservationDate,
					Status = reservation.Status.ToString()
				}
			});
		}

		// POST: api/Reservations
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPost]
		public async Task<ActionResult<Reservation>> PostReservation(ReservationCreateDto reservationDto)
		{
			// Kiểm tra dữ liệu đầu vào
			if (reservationDto == null)
			{
				return BadRequest("Invalid data.");
			}
			// Map dữ liệu từ ReservationDto sang Reservation
			var reservation = new Reservation
			{
				CustomerId = reservationDto.CustomerId,
				ServiceId = reservationDto.ServiceId,
				ChildName = reservationDto.ChildName,
				ReservationDate = DateTime.Parse($"{reservationDto.Date} {reservationDto.Time}"),
				Status = ReservationStatus.Pending,
				CreatedAt = DateTime.Now
			};
			_context.Reservations.Add(reservation);
			await _context.SaveChangesAsync();

			return CreatedAtAction("GetReservation", new { id = reservation.Id }, reservation);
		}

		// DELETE: api/Reservations/5
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteReservation(int id)
		{
			var reservation = await _context.Reservations.FindAsync(id);
			if (reservation == null)
			{
				return NotFound();
			}
			reservation.Status = ReservationStatus.Cancelled;
			await _context.SaveChangesAsync();

			var reservations = await _context.Reservations
				.Include(r => r.Service)
				.Include(r => r.Customer)
				.Select(r => new ReservationDto
				{
					Id = r.Id,
					ServiceName = r.Service.Name,
					CustomerName = r.Customer.FullName,
					ChildName = r.ChildName,
					CreateAt = r.CreatedAt,
					ReservationDate = r.ReservationDate,
					Status = r.Status.ToString(),
				})
			.ToListAsync();
			return Ok(reservations);
		}

		private bool ReservationExists(int id)
		{
			return _context.Reservations.Any(e => e.Id == id);
		}
	}
}
