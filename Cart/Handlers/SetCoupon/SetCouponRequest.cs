using MediatR;

namespace Cart.Handlers.SetCoupon
{
    public class SetCouponRequest : IRequest<Unit>
    {
        public int HeaderId { get; set; }
        public string CouponCode { get; set; }
    }
}