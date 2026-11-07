using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceOrder.Core.DTOs.Products
{
    public class ProductDetailDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string CategoryName { get; set; }
    }
}
