using ABCStore.Application.Services;
using ABCStore.Common.Responses.Products;
using ABCStore.Common.Wrappers;
using AutoMapper;
using MediatR;

namespace ABCStore.Application.Features.Products.Queries
{
    public class GetAllProductsQuery : IRequest<ResponseWrapper<List<ProductResponse>>>
    {
    }

    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, ResponseWrapper<List<ProductResponse>>>
    {
        private readonly IProductService _productSetvice;
        private readonly IMapper _mapper;
        public GetAllProductsQueryHandler(IProductService productSetvice, IMapper mapper)
        {
            _mapper = mapper;
            _productSetvice = productSetvice;
        }
        public async Task<ResponseWrapper<List<ProductResponse>>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await _productSetvice.GetAllAsync();
            if (products.Count() > 0)
            {
                var mappedProducts = _mapper.Map<List<ProductResponse>>(products);
                return new ResponseWrapper<List<ProductResponse>>().Success(mappedProducts);
            }
            return new ResponseWrapper<List<ProductResponse>>().Failed("ivalid request");
        }
    }
}
