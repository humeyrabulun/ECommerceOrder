using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using ECommerceOrder.Core.DTOs;
using ECommerceOrder.Core.DTOs.Categories;

namespace ECommerceBusiness.Validations
{
    public class CategoryCreateDtoValidator:AbstractValidator<CategoryCreateDto>
    {
        public CategoryCreateDtoValidator()
        {
            RuleFor(x=>x.Name)
                .NotEmpty().WithMessage("kategori adı boş geçilemez")
                .Length(3,50).WithMessage("kategori adı 3 ile 50 karakter arasında olmalıdır");
        }
    }
}
