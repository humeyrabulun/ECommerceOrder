using System;
using System.Collections.Generic;
using System.Text;
using ECommerceOrder.Core.DTOs.Products;


namespace ECommerceOrder.Core.Services
{
    public interface IProductService
    {
        Task<List<ProductListDto>> GetAllAsync();
        Task<ProductDetailDto> GetProductByIdAsync(int id);
        Task UpdateAsync(ProductUpdateDto productDto);
        Task DeleteAsync( int id );
        Task AddAsync(ProductCreateDto productDto);
      
    }
}
