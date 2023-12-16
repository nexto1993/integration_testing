using ABCStore.Application.Services;
using ABCStore.Common.Requests.Categories;
using ABCStore.Common.Wrappers;
using MediatR;

namespace ABCStore.Application.Features.Categories.Commands
{
    public class UpdateCategoryCommand : IRequest<ResponseWrapper<int>>
    {
        public UpdateCategoryRequest UpdateCategory { get; set; }
    }

    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, ResponseWrapper<int>>
    {
        private readonly ICategoryService _categorySetvice;

        public UpdateCategoryCommandHandler(ICategoryService categorySetvice)
        {
            _categorySetvice = categorySetvice;

        }
        public async Task<ResponseWrapper<int>> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var categoryIndb = await _categorySetvice.GetAsync(request.UpdateCategory.Id);
            if (categoryIndb.Id > 0)
            {
                categoryIndb.Name = request.UpdateCategory.Name;
                categoryIndb.Description = request.UpdateCategory.Description;

                var updatedCategory = await _categorySetvice.UpdateAsync(categoryIndb);
                return new ResponseWrapper<int>().Success(updatedCategory,
                    $"Category {categoryIndb.Name} updated successfully");

            }

            return new ResponseWrapper<int>().Failed("ivalid request");

        }
    }
}
