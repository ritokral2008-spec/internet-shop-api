namespace InternetShop.DTOs
{
    public class ProductDto
    {
        public required string Name { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
    }
}
