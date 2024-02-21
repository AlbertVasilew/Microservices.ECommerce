using Cart.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Cart.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Header> Headers { get; set; }
        public DbSet<Item> Items { get; set; }
    }
}