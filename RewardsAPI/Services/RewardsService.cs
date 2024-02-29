using MessageBus.Dtos;
using RewardsAPI.Data;
using RewardsAPI.Data.Models;

namespace RewardsAPI.Services
{
    public class RewardsService : IRewardsService
    {
        private readonly AppDbContext dbContext;

        public RewardsService(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task CreateReward(OrderCreatedMessageDto orderCreatedMessageDto)
        {
            await dbContext.AddAsync(new Reward
            {
                UserId = orderCreatedMessageDto.UserId,
                OrderId = orderCreatedMessageDto.OrderId,
                RewardActivity = orderCreatedMessageDto.RewardActivity
            });

            await dbContext.SaveChangesAsync();
        }
    }
}