using ABCStore.Application.Services;
using ABCStore.Common.Wrappers;
using MediatR;

namespace ABCStore.Application.Features.Products.Commands
{
    public class DeleteProductCommand : IRequest<ResponseWrapper<int>>
    {
        public int ProductId { get; set; }
    }
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, ResponseWrapper<int>>
    {
        private readonly IProductService _productService;
        public DeleteProductCommandHandler(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<ResponseWrapper<int>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var productIndb = await _productService.GetAsync(request.ProductId);
            if (productIndb.Id > 0)
            {
                var deletedProduct = await _productService.DeleteAsync(productIndb);
                return new ResponseWrapper<int>().Success(deletedProduct,
                    $"Product {productIndb.Name} deleted successfully");
            }

            return new ResponseWrapper<int>().Failed("ivalid request");
        }
    }
}
