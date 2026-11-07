namespace ECommerceOrder.Core.DTOs.Products
{
    public class ProductWithReviewDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public double AverageRating { get; set; }

        public List<string> Comments { get; set; } = new List<string>();
    }
}
