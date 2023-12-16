using ABCStore.Application.Services;
using ABCStore.Common.Responses.Categories;
using ABCStore.Common.Wrappers;
using AutoMapper;
using MediatR;

namespace ABCStore.Application.Features.Categories.Queries
{
    public class GetCategoryByIdQuery : IRequest<ResponseWrapper<CategoryResponse>>
    {
        public int CategoryId { get; set; }
    }

    public class GetCategoryByIdQueryhandler : IRequestHandler<GetCategoryByIdQuery, ResponseWrapper<CategoryResponse>>
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public GetCategoryByIdQueryhandler(ICategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;   
        }

        public async Task<ResponseWrapper<CategoryResponse>> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            var category = await _categoryService.GetAsync(request.CategoryId);
            if (category.Id > 0)
            {
                var mappedCategory = _mapper.Map<CategoryResponse>(category);
                return  new ResponseWrapper<CategoryResponse>().Success(mappedCategory);
            }
            return new ResponseWrapper<CategoryResponse>().Failed("ivalid request");
        }
    }
}
