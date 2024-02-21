using Cart.Contracts;
using Cart.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static Cart.Handlers.Get.GetCartHandler;

namespace Cart.Handlers.Get
{
    public partial class GetCartHandler : IRequestHandler<GetCartRequest, CartDto>
    {
        private readonly AppDbContext dbContext;
        private readonly ICouponService couponService;
        private readonly IProductService productService;

        public GetCartHandler(AppDbContext dbContext, ICouponService couponService, IProductService productService)
        {
            this.dbContext = dbContext;
            this.couponService = couponService;
            this.productService = productService;
        }

        public async Task<CartDto> Handle(GetCartRequest request, CancellationToken cancellationToken)
        {
            var header = await dbContext.Headers
                .Include(x => x.Items)
                .FirstOrDefaultAsync(x => x.UserId == request.UserId, cancellationToken);

            if (header == null)
                return null;

            var products = await productService.GetProducts();

            var cartDto = new CartDto
            {
                HeaderId = header.Id,
                CouponCode = header.CouponCode
            };

            foreach (var item in header.Items)
            {
                var product = products.FirstOrDefault(x => x.Id == item.ProductId);

                if (product == null) continue;

                cartDto.Total += item.Count * product.Price;
            }

            cartDto.BeforeDiscount = cartDto.Total;

            var coupons = await couponService.GetCoupons();
            var coupon = coupons.FirstOrDefault(x => x.Code == header.CouponCode);

            cartDto.Discount = coupon?.Discount ?? 0;
            cartDto.Total = cartDto.Total - cartDto.Discount;

            return cartDto;
        }
    }
}