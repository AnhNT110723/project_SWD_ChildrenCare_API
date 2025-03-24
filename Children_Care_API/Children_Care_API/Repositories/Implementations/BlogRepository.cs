using Children_Care_API.Data;
using Children_Care_API.Models;
using Children_Care_API.Repositories.Interfaces;

namespace Children_Care_API.Repositories.Implementations
{
    public class BlogRepository : BaseRepository<Blog>, IBlogRepository
    {
        public BlogRepository(ChildrenCareDbContext context) : base(context)
        {
        }
    }
}
