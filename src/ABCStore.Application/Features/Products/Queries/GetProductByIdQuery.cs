using ABCStore.Application.Services;
using ABCStore.Common.Responses.Products;
using ABCStore.Common.Wrappers;
using AutoMapper;
using MediatR;

namespace ABCStore.Application.Features.Products.Queries
{
    public class GetProductByIdQuery : IRequest<ResponseWrapper<ProductResponse>>
    {
        public int ProductId { get; set; }
    }

    public class GetProductByIdQueryHanlder : IRequestHandler<GetProductByIdQuery, ResponseWrapper<ProductResponse>>
    {
        private readonly IProductService _productSetvice;
        private readonly IMapper _mapper;
        public GetProductByIdQueryHanlder(IProductService productSetvice, IMapper mapper)
        {
            _mapper = mapper;
            _productSetvice = productSetvice;
        }
        public async Task<ResponseWrapper<ProductResponse>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await _productSetvice.GetAsync(request.ProductId);
            if (product.Id > 0)
            {
                var mappedProduct = _mapper.Map<ProductResponse>(product);
                return new ResponseWrapper<ProductResponse>().Success(mappedProduct);
            }
            return new ResponseWrapper<ProductResponse>().Failed("ivalid request");
        }
    }
}
