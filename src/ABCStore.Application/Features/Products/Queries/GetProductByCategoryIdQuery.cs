using ABCStore.Application.Services;
using ABCStore.Common.Responses.Products;
using ABCStore.Common.Wrappers;
using AutoMapper;
using MediatR;

namespace ABCStore.Application.Features.Products.Queries
{
    public class GetProductByCategoryIdQuery : IRequest<ResponseWrapper<List<ProductResponse>>>
    {
        public int CategoryId { get; set; }
    }

    public class GetProductByCategoryIdQueryHandler : IRequestHandler<GetProductByCategoryIdQuery, ResponseWrapper<List<ProductResponse>>>
    {
        private readonly IProductService _productSetvice;
        private readonly IMapper _mapper;
        public GetProductByCategoryIdQueryHandler(IProductService productSetvice, IMapper mapper)
        {
            _mapper = mapper;
            _productSetvice = productSetvice;
        }
        public async Task<ResponseWrapper<List<ProductResponse>>> Handle(GetProductByCategoryIdQuery request, CancellationToken cancellationToken)
        {
            var products = await _productSetvice.GetByCategoryAsync(request.CategoryId);
            if (products.Count() > 0)
            {
                var mappedProducts = _mapper.Map<List<ProductResponse>>(products);
                return new ResponseWrapper<List<ProductResponse>>().Success(mappedProducts);
            }
            return new ResponseWrapper<List<ProductResponse>>().Failed("ivalid request");
        }
    }
}
