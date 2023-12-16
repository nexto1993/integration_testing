using ABCStore.Application.Services;
using ABCStore.Common.Requests.Categories;
using ABCStore.Common.Wrappers;
using ABCStore.Domain.Entities;
using AutoMapper;
using MediatR;


namespace ABCStore.Application.Features.Categories.Commands
{
    public class CreateCategoryCommand : IRequest<ResponseWrapper<int>>
    {
        public CreateCategoryRequest CreateCategory { get; set; }

    }

    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, ResponseWrapper<int>>
    {
        private readonly ICategoryService _categorySetvice;
        private readonly IMapper _mapper;

        public CreateCategoryCommandHandler(IMapper mapper, ICategoryService categorySetvice)
        {
            _mapper = mapper;
            _categorySetvice = categorySetvice;

        }
        public async Task<ResponseWrapper<int>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            if (request.CreateCategory == null)
            {
                return new ResponseWrapper<int>().Failed("ivalid request");

            }
            var mappedCategory = _mapper.Map<Category>(request.CreateCategory);
            var newCategory = await _categorySetvice.CreateAsync(mappedCategory);
            return new ResponseWrapper<int>().Success(newCategory.Id, "Category createed successfully");
        }
    }
}
