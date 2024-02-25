namespace OrderAPI.Data.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public double Total { get; set; }
        public string CouponCode { get; set; }
        public double Discount { get; set; }
        public double BeforeDiscount { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        public ICollection<Product> Products { get; set; }
    }
}