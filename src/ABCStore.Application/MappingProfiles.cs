using ABCStore.Common.Requests.Categories;
using ABCStore.Common.Responses.Categories;
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

        }
    }
}
