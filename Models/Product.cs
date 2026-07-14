namespace InternetShop.Models
{
    public class Product
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = "";
        public Category Category { get; set; } = null!;
        public List<OrderItem> OrderItems { get; set; } = new();
    }
}
