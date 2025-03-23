using Children_Care_API.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Children_Care_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly ChildrenCareDbContext _context;

        public HomeController(ChildrenCareDbContext context)
        {
            _context = context;
        }

        // GET: api/home
        [HttpGet]
        public async Task<IActionResult> GetHomeData()
        {
            // Lấy sliders (chỉ lấy các slider đang active)
            var sliders = await _context.Sliders
                .Where(s => s.Status)
                .Select(s => new
                {
                    s.Id,
                    s.Title,
                    s.Image,
                    s.Backlink
                })
                .ToListAsync();

            // Lấy hot posts (lọc 3 bài viết có Status = true, sắp xếp theo CreatedAt giảm dần)
            var hotPosts = await _context.Blogs
                .Where(b => b.Status && b.IsFeatured)
                .OrderByDescending(b => b.CreatedAt)
                .Take(3)
                .Select(b => new
                {
                    b.Id,
                    b.Title,
                    Thumbnail = b.Thumbnail ?? "",
                    b.Category,
                    BriefInfo = b.Content.Substring(0, Math.Min(100, b.Content.Length)) // Lấy 100 ký tự đầu làm brief
                })
                .ToListAsync();

            // Lấy featured services (lọc 3 dịch vụ có Status = true)
            var featuredServices = await _context.Services
                .Where(s => s.Status && s.IsFeatured)
                .Take(3)
                .Select(s => new
                {
                    s.Id,
                    s.Name,
                    Thumbnail = s.Thumbnail ?? "",
                    s.Category,
                    BriefInfo = s.Description != null ? s.Description.Substring(0, Math.Min(100, s.Description.Length)) : "",
                    s.Price
                })
                .ToListAsync();

            // Lấy latest posts cho sidebar (lấy 5 bài mới nhất)
            var latestPosts = await _context.Blogs
                .Where(b => b.Status)
                .OrderByDescending(b => b.CreatedAt)
                .Take(5)
                .Select(b => new
                {
                    b.Id,
                    b.Title,
                    Thumbnail = b.Thumbnail ?? "",
                    b.Category,
                    BriefInfo = b.Content.Substring(0, Math.Min(100, b.Content.Length))
                })
                .ToListAsync();

            // Trả về dữ liệu tổng hợp
            return Ok(new
            {
                Sliders = sliders,
                HotPosts = hotPosts,
                FeaturedServices = featuredServices,
                LatestPosts = latestPosts
            });
        }
    }
}
