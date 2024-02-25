namespace Email.Data.Models
{
    public class EmailLog
    {
        public int Id { get; set; }
        public string Receiver { get; set; }
        public string Message { get; set; }
    }
}