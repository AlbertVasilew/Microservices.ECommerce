using Email.Data;
using Email.Data.Models;

namespace Email.Services
{
    public class EmailService : IEmailService
    {
        private readonly AppDbContext dbContext;

        public EmailService(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        private async Task CreateLog(string message, string receiver)
        {
            await dbContext.AddAsync(new EmailLog
            {
                Message = message,
                Receiver = receiver
            });

            await dbContext.SaveChangesAsync();
        }

        public async Task SentEmail(string message, string receiver)
        {
            await CreateLog(message, receiver);
        }
    }
}