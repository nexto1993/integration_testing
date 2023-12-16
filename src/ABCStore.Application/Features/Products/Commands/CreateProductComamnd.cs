using ABCStore.Application.Services;
using ABCStore.Common.Requests.Products;
using ABCStore.Common.Wrappers;
using ABCStore.Domain.Entities;
using AutoMapper;
using MediatR;

namespace ABCStore.Application.Features.Products.Commands
{
    public class CreateProductComamnd : IRequest<ResponseWrapper<int>>
    {
        public CreateProductRequest CreateProduct { get; set; }
    }

    public class CreateProductComamndHandler : IRequestHandler<CreateProductComamnd, ResponseWrapper<int>>
    {
        private readonly IProductService _productSetvice;
        private readonly IMapper _mapper;
        public CreateProductComamndHandler(IProductService productSetvice, IMapper mapper)
        {
            _mapper = mapper;
            _productSetvice = productSetvice;
        }
        public async Task<ResponseWrapper<int>> Handle(CreateProductComamnd request, CancellationToken cancellationToken)
        {
            if (request.CreateProduct == null)
            {
                return new ResponseWrapper<int>().Failed("ivalid request");
            }
            // test
            var productMapped = _mapper.Map<Product>(request.CreateProduct);
            var result = await _productSetvice.CreateAsync(productMapped);
            return new ResponseWrapper<int>().Success(result.Id, "Product createed successfully");

        }
    }
}
