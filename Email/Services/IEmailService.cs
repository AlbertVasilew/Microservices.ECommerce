
namespace Email.Services
{
    public interface IEmailService
    {
        Task SentEmail(string message, string receiver);
    }
}