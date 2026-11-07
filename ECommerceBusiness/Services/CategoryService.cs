using AutoMapper;
using ECommerceOrder.Core.DTOs.Categories;
using ECommerceOrder.Core.DTOs.Products;
using ECommerceOrder.Core.Models;
using ECommerceOrder.Core.Repositories;
using ECommerceOrder.Core.Services;
using ECommerceOrder.Core.UnitOfWorks;
using ECommerceOrder.Data;


namespace ECommerceBusiness.Services
{
    public class CategoryService: ICategoryService
    {
       
        private readonly ICategoryRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository repository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<CategoryListDto>> GetAllAsync()
        {
            var categories = _repository.GetAll().ToList();
            return _mapper.Map<List<CategoryListDto>>(categories);
        }

        public async Task<CategoryListDto> GetByIdAsync(int id)
        {
            var category = await _repository.GetCategoryWithProductsAsync(id);
            return _mapper.Map<CategoryListDto>(category);
        }

        public async Task AddAsync(CategoryCreateDto categoryDto)
        {
            var category = _mapper.Map<Category>(categoryDto);
            await _repository.AddAsync(category);
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteAsync(int id) { 
        
            var category=await _repository.GetByIdAsync(id);
            _repository.Delete(category);
            await _unitOfWork.CommitAsync();

        }

        public async Task UpdateAsync(CategoryUpdateDto categoryDto)
        {
            var category = _mapper.Map<Category>(categoryDto);
            _repository.Update(category);
            await _unitOfWork.CommitAsync();
        }

        public async Task<List<ProductWithReviewDto>> GetProductsWithMinRatingAsync(int categoryId, int minRating)
        {
            
            var category = await _repository.GetCategoryWithProductsAsync(categoryId);

         
            if (category == null || category.Products == null)
            {
                return new List<ProductWithReviewDto>();
            }

            // 3. Dönüştürme ve filtreleme işlemleri
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

            return result;
        }
    }
}
