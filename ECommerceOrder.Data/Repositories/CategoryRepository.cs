using ECommerceOrder.Core.Models;
using ECommerceOrder.Core.Repositories;
using ECommerceOrder.Data;
using Microsoft.EntityFrameworkCore;

namespace ECommerceOrder.Data.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<Category> GetCategoryWithProductsAsync(int Id)
        {
            return await _context.Categories
                                 .Include(c => c.Products)
                                 .FirstOrDefaultAsync(c => c.Id == Id);
        }
    }
}