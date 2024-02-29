using Microsoft.EntityFrameworkCore;
using RewardsAPI.Data.Models;

namespace RewardsAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Reward> Rewards { get; set; }
    }
}