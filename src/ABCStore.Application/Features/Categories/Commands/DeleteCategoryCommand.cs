using ABCStore.Application.Services;
using ABCStore.Common.Wrappers;
using MediatR;

namespace ABCStore.Application.Features.Categories.Commands
{
    public class DeleteCategoryCommand : IRequest<ResponseWrapper<int>>
    {
        public int CategoryId { get; set; }
    }

    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, ResponseWrapper<int>>
    {
        private readonly ICategoryService _categoryService;

        public DeleteCategoryCommandHandler(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<ResponseWrapper<int>> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var categoryIndb = await _categoryService.GetAsync(request.CategoryId);
            if (categoryIndb.Id > 0) 
            {
                var deletedCategory = await _categoryService.DeleteAsync(categoryIndb);
                return new ResponseWrapper<int>().Success(deletedCategory,
                    $"Category {categoryIndb.Name} deleted successfully");
            }

            return new ResponseWrapper<int>().Failed("ivalid request");
        }
    }
}
