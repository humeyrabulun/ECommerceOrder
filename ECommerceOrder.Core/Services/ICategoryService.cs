using System;
using System.Collections.Generic;
using System.Text;
using ECommerceOrder.Core.DTOs.Categories;
using ECommerceOrder.Core.DTOs.Products;

namespace ECommerceOrder.Core.Services
{
    public interface ICategoryService
    {
        Task<List<CategoryListDto>> GetAllAsync();
        Task<CategoryListDto> GetByIdAsync(int id);
        Task AddAsync(CategoryCreateDto categoryDto);
        Task UpdateAsync(CategoryUpdateDto categoryDto);
        Task DeleteAsync(int id);
        Task <List<ProductWithReviewDto>> GetProductsWithMinRatingAsync(int id,int minRating);
    }
}
