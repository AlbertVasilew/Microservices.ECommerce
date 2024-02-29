
using MessageBus.Dtos;

namespace RewardsAPI.Services
{
    public interface IRewardsService
    {
        Task CreateReward(OrderCreatedMessageDto orderCreatedMessageDto);
    }
}