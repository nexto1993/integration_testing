using ABCStore.Common.Requests.Categories;
using ABCStore.Common.Requests.Products;
using ABCStore.Common.Responses.Categories;
using ABCStore.Common.Responses.Products;
using ABCStore.Domain.Entities;
using AutoMapper;

namespace ABCStore.Application
{
    public class MappingProfiles : Profile
    {
        protected MappingProfiles()
        {
            CreateMap<CreateCategoryRequest, Category>();
            CreateMap<Category, CategoryResponse>().ReverseMap();
            CreateMap<CreateProductRequest, Product>().ReverseMap();
            CreateMap<Product, ProductResponse>().ReverseMap();


        }
    }
}
