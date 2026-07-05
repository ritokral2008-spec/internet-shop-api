namespace InternetShop.Models
{
    public class Order
    {
        public int Id { get; set; }
        public List<OrderProduct> Products { get; set; } = new();
    }
}
