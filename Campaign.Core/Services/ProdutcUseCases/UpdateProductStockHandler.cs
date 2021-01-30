using Campaign.Core.Dtos;
using Campaign.Core.Events;
using Campaign.Core.Interfaces;
using Campaign.Core.Models;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Campaign.Core.Services.ProdutcUseCases
{
    public class UpdateProductStockHandler : INotificationHandler<OrderCreatedEvent>
    {
        private readonly IRepository<Product> _repository;
        private readonly ILogger<GetProductHandler> _logger; 

        public UpdateProductStockHandler(IRepository<Product> repository, ILogger<GetProductHandler> logger, IMediator mediator)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task Handle(OrderCreatedEvent notification, CancellationToken cancellationToken)
        {
            BaseResponseDto<ProductDto> response = new BaseResponseDto<ProductDto>();

            var product = (await _repository.GetWhereAsync(p => p.ProductCode == notification.ProductCode)).FirstOrDefault();

            product.Stock -= notification.Quantity;

            await _repository.UpdateAsync(product);
        }
    }
}

