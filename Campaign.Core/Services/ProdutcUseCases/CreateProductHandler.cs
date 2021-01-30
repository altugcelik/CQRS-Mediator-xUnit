using Campaign.Core.Dtos;
using Campaign.Core.Dtos.Requests;
using Campaign.Core.Events;
using Campaign.Core.Interfaces;
using Campaign.Core.Models;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Campaign.Core.Services.ProdutcUseCases
{
    public class CreateProductHandler : IRequestHandler<CreateProductRequest, BaseResponseDto<bool>>
    {
        private readonly IRepository<Product> _repository;
        private readonly ILogger<CreateProductHandler> _logger;
        private readonly IMediator _mediator;

        public CreateProductHandler(IRepository<Product> repository, ILogger<CreateProductHandler> logger, IMediator mediator)
        {
            _repository = repository;
            _logger = logger;
            _mediator = mediator;
        }

        public async Task<BaseResponseDto<bool>> Handle(CreateProductRequest request, CancellationToken cancellationToken)
        {
            BaseResponseDto<bool> response = new BaseResponseDto<bool>();

            if (request.Price <= 0)
            {
                throw new ArgumentException($"{nameof(request.Price)} should greater than 0");
            }

            try
            {              
                var product = new Product
                {
                    ProductCode = request.ProductCode,
                    Price = request.Price,
                    Stock = request.Stock
                };

                await _repository.CreateAsync(product);

                response.Data = true;

                await _mediator.Publish(new ProductCreatedEvent(productName: product.ProductCode));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                response.Errors.Add("An error occurred while creating the product.");
            }

            return response;
        }
    }
}
