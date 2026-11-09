using System;
using System.Collections.Generic;
using System.Text;
using ECommerceOrder.Core.DTOs.Categories;
using ECommerceOrder.Core.DTOs.Products;

namespace ECommerceOrder.Core.Services
{
    public interface ICategoryService
    {
        Task<Result<List<CategoryListDto>>> GetAllAsync();

        Task<Result<CategoryListDto>> GetByIdAsync(int id);

        Task<Result<CategoryCreateDto>> AddAsync(CategoryCreateDto categoryDto);

        Task<Result<CategoryUpdateDto>> UpdateAsync(CategoryUpdateDto categoryDto);

        Task<Result<string>> DeleteAsync(int id);

        Task<Result<List<ProductWithReviewDto>>> GetProductsWithMinRatingAsync(int categoryId, int minRating);


    }
}
