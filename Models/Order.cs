namespace InternetShop.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string Status { get; set; } = "Pending";
        public decimal TotalPrice { get; set; }
        public List<OrderItem> Items { get; set; } = new();
    }
}
