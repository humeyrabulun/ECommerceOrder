namespace ECommerceOrder.Core.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string? Barcode { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; } = null!;
  
        public ICollection<ProductTag> ProductTags { get; set; } = new List<ProductTag>();

        public ICollection<Review> Reviews { get; set; } = new List<Review>();
    }
}
