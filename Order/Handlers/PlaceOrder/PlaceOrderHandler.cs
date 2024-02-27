using MediatR;
using OrderAPI.Data;
using OrderAPI.Data.Models;

namespace OrderAPI.Handlers.PlaceOrder
{
    public class PlaceOrderHandler : IRequestHandler<PlaceOrderRequest, Unit>
    {
        private readonly AppDbContext dbContext;

        public PlaceOrderHandler(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
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

            await dbContext.AddAsync(order, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}