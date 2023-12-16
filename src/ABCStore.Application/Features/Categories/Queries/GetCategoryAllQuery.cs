using ABCStore.Application.Services;
using ABCStore.Common.Responses.Categories;
using ABCStore.Common.Wrappers;
using AutoMapper;
using MediatR;

namespace ABCStore.Application.Features.Categories.Queries
{
    public class GetCategoryAllQuery : IRequest<ResponseWrapper<List<CategoryResponse>>>
    {
    }

    public class GetCategoryAllQueryQueryhandler : IRequestHandler<GetCategoryAllQuery, ResponseWrapper<List<CategoryResponse>>>
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public GetCategoryAllQueryQueryhandler(ICategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;   
        }

        public async Task<ResponseWrapper<List<CategoryResponse>>> Handle(GetCategoryAllQuery request, CancellationToken cancellationToken)
        {
            var categories = await _categoryService.GetAllAsync();
            if (categories.Any())
            {
                var mappedCategory = _mapper.Map<List<CategoryResponse>>(categories);
                return new ResponseWrapper<List<CategoryResponse>>().Success(mappedCategory);
            }
            return new ResponseWrapper<List<CategoryResponse>>().Failed("ivalid request");
        }
    }
}
