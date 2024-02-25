using Email.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Email.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<EmailLog> EmailLogs { get; set; }
    }
}