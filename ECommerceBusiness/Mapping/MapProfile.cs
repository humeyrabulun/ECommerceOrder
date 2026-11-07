using AutoMapper;
using ECommerceOrder.Core.DTOs.Products;
using ECommerceOrder.Core.DTOs.Categories;
using ECommerceOrder.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceBusiness.Mapping
{
    public class MapProfile:Profile
    {
        public MapProfile()
        {
            CreateMap<Product, ProductListDto>().ReverseMap();
            CreateMap<Product, ProductDetailDto>().ReverseMap();
            CreateMap<Product, ProductCreateDto>().ReverseMap();
            CreateMap<Product, ProductUpdateDto>().ReverseMap();

            CreateMap<Category, CategoryListDto>().ReverseMap();
            CreateMap<Category, CategoryListDto>().ReverseMap();
            CreateMap<Category, CategoryCreateDto>().ReverseMap();
            CreateMap<Category, CategoryUpdateDto>().ReverseMap();
        }   
    }
}
