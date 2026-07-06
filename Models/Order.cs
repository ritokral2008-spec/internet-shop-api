namespace InternetShop.Models
{
    public class Order
    {
        public int Id { get; set; }
        public List<OrderItem> Products { get; set; } = new();
    }
}
