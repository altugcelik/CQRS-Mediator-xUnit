using Campaign.Core.Dtos;
using Campaign.Core.Dtos.Requests;
using Campaign.Core.Interfaces;
using Campaign.Core.Models;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Campaign.Core.Services.ProdutcUseCases
{
    public class GetProductHandler : IRequestHandler<GetProductRequest, BaseResponseDto<ProductDto>>
    {
        private readonly IRepository<Product> _repository;
        private readonly ILogger<GetProductHandler> _logger;
        private readonly IMediator _mediator;

        public GetProductHandler(IRepository<Product> repository, ILogger<GetProductHandler> logger, IMediator mediator)
        {
            _repository = repository;
            _logger = logger;
            _mediator = mediator;
        }

        public async Task<BaseResponseDto<ProductDto>> Handle(GetProductRequest request, CancellationToken cancellationToken)
        {
            BaseResponseDto<ProductDto> response = new BaseResponseDto<ProductDto>();

            try
            {
                ProductDto product = (await _repository.GetWhereAsync(p => p.ProductCode == request.productCode)).Select(p => new ProductDto
                {
                    ProductCode = p.ProductCode,
                    Price = p.Price,
                    Stock = p.Stock
                }).FirstOrDefault();

                BaseResponseDto<CampaignDto> getCampaignReponse = await _mediator.Send(new GetCampaignByProductCodeRequest(productCode: product.ProductCode));

                if (getCampaignReponse != null)
                {
                    if (getCampaignReponse.Data != null)
                    {
                        product.Price = product.Price - (product.Price * (getCampaignReponse.Data.Limit / 100M));
                    }
                }
                response.Data = product;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                response.Errors.Add("An error occurred while getting product.");
            }

            return response;
        }
    }
}
