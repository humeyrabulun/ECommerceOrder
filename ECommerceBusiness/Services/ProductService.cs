using ECommerceOrder.Data;
using ECommerceOrder.Core.Models;
using ECommerceOrder.Core.DTOs.Products;
using AutoMapper;
using ECommerceOrder.Core.Services;
using ECommerceOrder.Core.UnitOfWorks;
using ECommerceOrder.Core.DTOs.Products;
using ECommerceOrder.Core.Repositories;

namespace ECommerceBusiness.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;


        public ProductService(IProductRepository repository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<ProductListDto>> GetAllAsync()
        {

            var products = _repository.GetAll().ToList();
            return _mapper.Map<List<ProductListDto>>(products);
        }

        public async Task<ProductDetailDto> GetProductByIdAsync(int id)
        {
            var product = await _repository.GetProductWithDetailsAsync(id);
            return _mapper.Map<ProductDetailDto>(product);
        }

        public async Task AddAsync(ProductCreateDto productDto)
        {
            var product = _mapper.Map<Product>(productDto);

            await _repository.AddAsync(product);
            await _unitOfWork.CommitAsync();
        }


        public async Task UpdateAsync( ProductUpdateDto productDto)
        {
            var product = _mapper.Map<Product>(productDto);

            _repository.Update(product);
            await _unitOfWork.CommitAsync();


        }

        public async Task DeleteAsync(int id)
        {
            var product = await _repository.GetByIdAsync(id);
            if (product != null)
            {
                _repository.Delete(product);
                await _unitOfWork.CommitAsync();

            }


        }

       
    }
}
