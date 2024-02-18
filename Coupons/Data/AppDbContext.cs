using Coupons.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Coupons.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Coupon> Coupons { get; set; }
    }
}