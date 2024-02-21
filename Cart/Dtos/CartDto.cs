namespace Cart.Handlers.Get
{
    public partial class GetCartHandler
    {
        public class CartDto
        {
            public int HeaderId { get; set; }
            public string CouponCode { get; set; }
            public double Total { get; set; }
            public double Discount { get; set; }
            public double BeforeDiscount { get; set; }
        }
    }
}