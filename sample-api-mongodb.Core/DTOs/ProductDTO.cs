namespace sample_api_mongodb.Core.DTOs
{
    public class ProductDTO
    {
        public string? Id { get; set; }
        public int ProductId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Price { get; set; }
        public double DiscountPercentage { get; set; }
        public double Rating { get; set; }
        public int Stock { get; set; }
        public string Brand { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string Thumbnail { get; set; } = string.Empty;
        public List<string>? images { get; set; }
    }
}
