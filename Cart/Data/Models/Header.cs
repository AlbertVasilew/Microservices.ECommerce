namespace Cart.Data.Models
{
    public class Header
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string? CouponCode { get; set; }

        public ICollection<Item> Items { get; set; } = new List<Item>();
    }
}