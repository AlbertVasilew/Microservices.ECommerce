using MediatR;
using MessageBus;
using Microsoft.Extensions.Configuration;
using OrderAPI.Data;
using OrderAPI.Data.Models;

namespace OrderAPI.Handlers.PlaceOrder
{
    public class PlaceOrderHandler : IRequestHandler<PlaceOrderRequest, Unit>
    {
        private readonly AppDbContext dbContext;
        private readonly IMessageBusSender messageBusSender;
        private readonly IConfiguration configuration;

        public PlaceOrderHandler(
            AppDbContext dbContext, IMessageBusSender messageBusSender, IConfiguration configuration)
        {
            this.dbContext = dbContext;
            this.messageBusSender = messageBusSender;
            this.configuration = configuration;
        }

        public async Task<Unit> Handle(PlaceOrderRequest request, CancellationToken cancellationToken)
        {
            var order = new Order
            {
                UserId = request.UserId,
                Total = request.Total,
                CouponCode = request.CouponCode,
                Discount = request.Discount,
                BeforeDiscount = request.BeforeDiscount,
                Products = request.Products.Select(
                    x => new Product { Name = x.Name, Price = x.Price, ProductId = x.ProductId, Count = x.Count }).ToList()
            };

            messageBusSender.SendExchangeMessage(
                request.UserId, configuration.GetValue<string>("MessageBusQueuesTopics:OrderCreatedTopic"));

            await dbContext.AddAsync(order, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}