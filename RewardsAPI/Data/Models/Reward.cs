namespace RewardsAPI.Data.Models
{
    public class Reward
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int OrderId { get; set; }
        public double RewardActivity { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
    }
}