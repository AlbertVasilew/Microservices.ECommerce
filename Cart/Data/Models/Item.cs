namespace Cart.Data.Models
{
    public class Item
    {
        public int Id { get; set; }
        public Header Header { get; set; }
        public int ProductId { get; set; }
        public int Count { get; set; }
    }
}