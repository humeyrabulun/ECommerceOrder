using ECommerceOrder.Core.DTOs.Products;
using ECommerceOrder.Core.Repositories;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceBusiness.Validations
{
    public class ProductCreateDtoValidator:AbstractValidator<ProductCreateDto>
    {
        private readonly ICategoryRepository _categoryRepository;

        public ProductCreateDtoValidator(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;

            RuleFor(x=>x.Name)
                .NotEmpty().WithMessage("Ürün adı boş olamaz.")
                .Length(3, 100).WithMessage("Ürün adı 3 ile 100 karakter arasında olmalıdır.");

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("Fiyat 0'dan büyük olmalıdır.");

            RuleFor(x => x.CategoryId)
                .GreaterThan(0).WithMessage("Geçerli bir Kategori ID giriniz.")

                .MustAsync(async (categoryId, cancellationToken) =>
                {
                    var category = await _categoryRepository.GetByIdAsync(categoryId);
                    return category != null;
                }).WithMessage("Girdiğiniz Kategori ID'si veritabanında bulunamadı.");
        }



    }
  }

