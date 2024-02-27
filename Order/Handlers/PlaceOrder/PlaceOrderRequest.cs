using MediatR;
using OrderAPI.Dtos;

namespace OrderAPI.Handlers.PlaceOrder
{
    public class PlaceOrderRequest : IRequest<Unit>
    {
        public string UserId { get; set; }
        public double Total { get; set; }
        public string CouponCode { get; set; }
        public double Discount { get; set; }
        public double BeforeDiscount { get; set; }
        public IList<ProductDto> Products { get; set; } = new List<ProductDto>();

    }
}