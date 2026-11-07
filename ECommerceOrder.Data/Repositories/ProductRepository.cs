using ECommerceOrder.Core.Models;
using ECommerceOrder.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using ECommerceOrder.Data;

namespace ECommerceOrder.Data.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
       
        public ProductRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<Product> GetProductWithDetailsAsync(int Id)
        {
            return await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Reviews)
                .FirstOrDefaultAsync(p => p.Id == Id);
        }
    }
}