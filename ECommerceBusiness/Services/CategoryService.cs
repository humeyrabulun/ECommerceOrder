using AutoMapper;
using ECommerceOrder.Core;
using ECommerceOrder.Core.DTOs.Categories;
using ECommerceOrder.Core.DTOs.Products;
using ECommerceOrder.Core.Models;
using ECommerceOrder.Core.Repositories;
using ECommerceOrder.Core.Services;
using ECommerceOrder.Core.UnitOfWorks;
using ECommerceOrder.Data;
using FluentValidation;
using System.ComponentModel.DataAnnotations;


namespace ECommerceBusiness.Services
{
    public class CategoryService: ICategoryService

    {
       
        private readonly ICategoryRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;
        private readonly IValidator<CategoryCreateDto> _createDtovalidator;

        public CategoryService(ICategoryRepository repository, IUnitOfWork unitOfWork, IMapper mapper, IValidator<CategoryCreateDto> createDtovalidator )
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _createDtovalidator = createDtovalidator;
        }

        public async Task<Result<List<CategoryListDto>>> GetAllAsync()
        {
            var categories = _repository.GetAll().ToList();
            var dtoList= _mapper.Map<List<CategoryListDto>>(categories);

            return Result<List<CategoryListDto>>.Success(dtoList, 200);
        }

        public async Task<Result<CategoryListDto>> GetByIdAsync(int id)
        {
            var category = await _repository.GetCategoryWithProductsAsync(id);

            if(category == null)
            {
                return Result<CategoryListDto>.Fail("Kategori bulunamadı", 404);
            }
            var dto= _mapper.Map<CategoryListDto>(category);
            return Result<CategoryListDto >.Success(dto, 200);
        }

        public async Task<Result<CategoryCreateDto>> AddAsync(CategoryCreateDto categoryDto)
        {
            var ValidationResult = await _createDtovalidator.ValidateAsync(categoryDto);

            if (!ValidationResult.IsValid)
            {
                var errors= ValidationResult.Errors.Select(x => x.ErrorMessage).ToList();
                return Result<CategoryCreateDto>.Fail(errors, 400);
            }
            var category = _mapper.Map<Category>(categoryDto);
            await _repository.AddAsync(category);
            await _unitOfWork.CommitAsync();
            return Result<CategoryCreateDto>.Success(categoryDto, 201);
        }

        public async Task<Result<string>> DeleteAsync(int id) { 
        
            var category=await _repository.GetByIdAsync(id);

            if (category == null)
            {
                return Result<string>.Fail("Kategori bulunamadı", 404);
            }
            _repository.Delete(category);
            await _unitOfWork.CommitAsync();
            return Result<string>.Success("Kategori başarıyla silindi", 200);

        }

        public async Task<Result<CategoryUpdateDto>> UpdateAsync(CategoryUpdateDto categoryDto)
        {
            var category = await _repository.GetByIdAsync(categoryDto.Id);

            if (category == null)
            {
                return Result<CategoryUpdateDto>.Fail("Kategori bulunamadı", 404);
            }
            _mapper.Map(categoryDto, category);
            _repository.Update(category);
            await _unitOfWork.CommitAsync();
            return Result<CategoryUpdateDto>.Success(categoryDto, 200);
        }

        public async Task<Result<List<ProductWithReviewDto>>> GetProductsWithMinRatingAsync(int categoryId, int minRating)
        {
            
            var category = await _repository.GetCategoryWithProductsAsync(categoryId);

         
            if (category == null || category.Products == null)
            {
                return Result<List<ProductWithReviewDto>>.Fail("Kategori veya ürünler bulunamadı", 404);
            }

           
            var result = category.Products
                .Where(p => p.Reviews != null && p.Reviews.Any() && p.Reviews.Average(r => r.Rating) >= minRating)
                .Select(p => new ProductWithReviewDto
                {
                    ProductId = p.Id,
                    ProductName = p.Name,
                    Price = p.Price,
                    AverageRating = p.Reviews.Average(r => r.Rating),
                    Comments = p.Reviews.Select(r => r.Comment).ToList()
                })
                .ToList();

            return Result<List<ProductWithReviewDto>>.Success(result, 200);
        }
    }
}
