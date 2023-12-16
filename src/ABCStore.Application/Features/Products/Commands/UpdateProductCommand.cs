using ABCStore.Application.Services;
using ABCStore.Common.Requests.Products;
using ABCStore.Common.Wrappers;
using MediatR;

namespace ABCStore.Application.Features.Products.Commands
{
    public class UpdateProductCommand : IRequest<ResponseWrapper<int>>
    {
        public UpdateProductRequest UpdateProduct { get; set; }
    }

    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, ResponseWrapper<int>>
    {
        private readonly IProductService _productService;
        public UpdateProductCommandHandler(IProductService productService)
        {
            _productService = productService;
        }
        public async Task<ResponseWrapper<int>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var productInDb = await _productService.GetAsync(request.UpdateProduct.Id);
            if (productInDb.Id > 0 )
            {
                productInDb.Name = request.UpdateProduct.Name;
                productInDb.Description = request.UpdateProduct.Description;
                productInDb.Price = request.UpdateProduct.Price;
                productInDb.CategoryId = request.UpdateProduct.CategoryId;

                var updatedProduct = await _productService.UpdateAsync(productInDb);
                return new ResponseWrapper<int>().Success(updatedProduct,
                    $"Product {productInDb.Name} updated successfully");
            }
            return new ResponseWrapper<int>().Failed("ivalid request");

        }
    }
}
