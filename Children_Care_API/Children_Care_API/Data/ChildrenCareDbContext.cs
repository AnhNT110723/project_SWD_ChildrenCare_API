using Microsoft.EntityFrameworkCore;
using PaymentAPI.Models;

namespace Children_Care_API.Data
{
    public class ChildrenCareDbContext : DbContext
    {
        public ChildrenCareDbContext(DbContextOptions<ChildrenCareDbContext> options) : base(options)
        {
        }
        public DbSet<PaymentDetail> PaymentDetails { get; set; }
    }
}
