using Cart.Dtos;

namespace Cart.Contracts
{
    public interface ICouponService
    {
        Task<IList<CouponDto>> GetCoupons();
    }
}