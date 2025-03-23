using Children_Care_API.Data;
using Children_Care_API.DTOs;
using Children_Care_API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Children_Care_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Manager")]
    public class SlidersController : ControllerBase
    {
        private readonly ChildrenCareDbContext _context;

        public SlidersController(ChildrenCareDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> GetSliders([FromQuery] int page = 1, [FromQuery] int pageSize = 2, [FromQuery] string? search = null)
        {
            var query = _context.Sliders.AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(s => s.Title.Contains(search) || s.Backlink.Contains(search));
            }

            var totalItems = await query.CountAsync();
            var sliders = await query
                .OrderBy(s => s.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(s => new
                {
                    s.Id,
                    s.Title,
                    s.Image,
                    s.Backlink,
                    s.Status
                })
                .ToListAsync();

            return Ok(new
            {
                TotalItems = totalItems,
                Page = page,
                PageSize = pageSize,
                Sliders = sliders
            });
        }

        [HttpPost]
        public async Task<IActionResult> AddSlider([FromBody] SliderDto sliderDto)
        {
            var slider = new Slider
            {
                Title = sliderDto.Title,
                Image = sliderDto.Image,
                Backlink = sliderDto.Backlink,
                Status = sliderDto.Status
            };
            _context.Sliders.Add(slider);
            await _context.SaveChangesAsync();
            return Ok(slider);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSlider(int id)
        {
            var slider = await _context.Sliders.FindAsync(id);
            if (slider == null) return NotFound();

            _context.Sliders.Remove(slider);
            await _context.SaveChangesAsync();
            return Ok("Slider deleted.");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSliderDetails(int id)
        {
            var slider = await _context.Sliders
                .Where(s => s.Id == id)
                .Select(s => new
                {
                    s.Id,
                    s.Title,
                    s.Image,
                    s.Backlink,
                    s.Status
                })
                .FirstOrDefaultAsync();

            if (slider == null) return NotFound();
            return Ok(slider);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSlider(int id, [FromBody] SliderDto sliderDto)
        {
            var slider = await _context.Sliders.FindAsync(id);
            if (slider == null) return NotFound();

            slider.Title = sliderDto.Title;
            slider.Image = sliderDto.Image;
            slider.Backlink = sliderDto.Backlink;
            slider.Status = sliderDto.Status;

            await _context.SaveChangesAsync();
            return Ok(slider);
        }
    }
}
