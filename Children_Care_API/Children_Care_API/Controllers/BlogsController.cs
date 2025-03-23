using Children_Care_API.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Children_Care_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogsController : ControllerBase
    {
        private readonly ChildrenCareDbContext _context;

        public BlogsController(ChildrenCareDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> GetBlogs([FromQuery] int page = 1, [FromQuery] int pageSize = 2, [FromQuery] string? search = null)
        {
            var query = _context.Blogs
                .Where(b => b.Status); // Chỉ lấy bài viết active

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(b => b.Title.Contains(search) || b.BriefInfo.Contains(search));
            }
            query = query.OrderByDescending(b => b.CreatedAt);


            var totalItems = await query.CountAsync();
            var blogs = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(b => new
                {
                    b.Id,
                    b.Title,
                    b.Thumbnail,
                    b.BriefInfo,
                    b.CreatedAt
                })
                .ToListAsync();

            return Ok(new
            {
                TotalItems = totalItems,
                Page = page,
                PageSize = pageSize,
                Blogs = blogs
            });
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBlogDetails(int id)
        {
            var blog = await _context.Blogs
                .Where(b => b.Status && b.Id == id)
                .Select(b => new
                {
                    b.Id,
                    b.Title,
                    b.Content,
                    b.Category,
                    b.Thumbnail,
                    Author = b.Author.FullName
                })
                .FirstOrDefaultAsync();

            if (blog == null) return NotFound();
            return Ok(blog);
        }
    }
}
