using ECommerceOrder.Data;
using ECommerceOrder.Core.Models;
using ECommerceOrder.Core.DTOs.Products;
using AutoMapper;
using ECommerceOrder.Core.Services;
using ECommerceOrder.Core.UnitOfWorks;
using ECommerceOrder.Core.DTOs.Products;
using ECommerceOrder.Core.Repositories;
using FluentValidation;
using ECommerceOrder.Core;


namespace ECommerceBusiness.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IValidator<ProductCreateDto> _createValidator;


        public ProductService(IProductRepository repository, IUnitOfWork unitOfWork, IMapper mapper,IValidator<ProductCreateDto>createDtoValidator)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _createValidator = createDtoValidator;
        }

        public async Task<Result<List<ProductListDto>>> GetAllAsync()
        {
            
            var products = _repository.GetAll().ToList();
            var productList= _mapper.Map<List<ProductListDto>>(products);

            return Result<List<ProductListDto>>.Success(productList, 200);
        }

        public async Task<Result<ProductDetailDto>> GetProductByIdAsync(int id)
        {
            var product = await _repository.GetProductWithDetailsAsync(id);

            if (product == null)
            {
                return Result<ProductDetailDto>.Fail("ürün bulunamadı", 404);
            }
            var productDto= _mapper.Map<ProductDetailDto>(product);
            return Result<ProductDetailDto>.Success(productDto, 200);
        }

        public async Task<Result<ProductCreateDto>> AddAsync(ProductCreateDto productDto)
        {
            var validationResult = await _createValidator .ValidateAsync(productDto);

            if(!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return Result<ProductCreateDto>.Fail(errors, 400);
            }

            var product=_mapper.Map<Product>(productDto);
            await _repository.AddAsync(product);
            await _unitOfWork.CommitAsync();

            return Result<ProductCreateDto>.Success(productDto, 201);
        }


        public async Task<Result<ProductUpdateDto>> UpdateAsync(ProductUpdateDto productDto)
        {
            var product = await _repository.GetByIdAsync(productDto.Id);
            if (product == null)
            {
                return Result<ProductUpdateDto>.Fail("Güncellenecek ürün bulunamadı.", 404);
            }

            
            _mapper.Map(productDto, product);

            _repository.Update(product);
            await _unitOfWork.CommitAsync();

            return Result<ProductUpdateDto>.Success(productDto, 204);
        }
        public async Task<Result<string>> DeleteAsync(int id)
        {
            var product = await _repository.GetByIdAsync(id);
            if (product != null)
            {
                _repository.Delete(product);
                await _unitOfWork.CommitAsync();
                return Result<string>.Success("Silindi", 204);

            }

            return Result<string>.Fail("Silinecek ürün bulunamadı.", 404);
        }

       
    }
}
