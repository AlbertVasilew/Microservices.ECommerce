namespace MessageBus.Dtos
{
    public class OrderCreatedMessageDto
    {
        public string UserId { get; set; }
        public double RewardActivity { get; set; }
        public int OrderId { get; set; }
    }
}
