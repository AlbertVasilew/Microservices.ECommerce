using MediatR;
using OrderAPI.Data;

namespace OrderAPI.Handlers.PlaceOrder
{
    public class PlaceOrderRequest : IRequest<Unit>
    {
        public int CartId { get; set; }
    }

    public class PlaceOrderHandler : IRequestHandler<PlaceOrderRequest, Unit>
    {
        private readonly AppDbContext dbContext;

        public PlaceOrderHandler(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Unit> Handle(PlaceOrderRequest request, CancellationToken cancellationToken)
        {
            return Unit.Value;
        }
    }
}