using System;
using System.Collections.Generic;
using System.Text;
using ECommerceOrder.Core.DTOs.Products;


namespace ECommerceOrder.Core.Services
{
    public interface IProductService
    {
        Task<Result<List<ProductListDto>>> GetAllAsync();
        Task<Result<ProductDetailDto>> GetProductByIdAsync(int id);
        Task<Result<ProductUpdateDto>> UpdateAsync(ProductUpdateDto productDto);
        Task<Result<string>> DeleteAsync( int id );
        Task<Result<ProductCreateDto>> AddAsync(ProductCreateDto productDto);
      
    }
}
