using Cart.Contracts;
using Cart.Dtos;
using Newtonsoft.Json;

namespace Cart.Services
{
    public class CouponService : ICouponService
    {
        private readonly IHttpClientFactory httpClientFactory;

        public CouponService(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        public async Task<IList<CouponDto>> GetCoupons()
        {
            var client = httpClientFactory.CreateClient("Coupon");
            var response = await client.GetAsync("/api/coupons");
            var content = await response.Content.ReadAsStringAsync();

            var coupons = new List<CouponDto>();

            if (response.IsSuccessStatusCode)
                coupons = JsonConvert.DeserializeObject<IList<CouponDto>>(content)?.ToList();

            return coupons;
        }
    }
}