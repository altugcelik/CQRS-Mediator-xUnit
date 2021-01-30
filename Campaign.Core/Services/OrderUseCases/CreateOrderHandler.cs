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

namespace Campaign.Core.Services.OrderUseCases
{
    public class CreateOrderHandler : IRequestHandler<CreateOrderRequest, BaseResponseDto<bool>>
    {
        private readonly IRepository<Order> _repository;
        private readonly ILogger<CreateOrderHandler> _logger;
        private readonly IMediator _mediator;

        public CreateOrderHandler(IRepository<Order> repository, ILogger<CreateOrderHandler> logger, IMediator mediator)
        {
            _repository = repository;
            _logger = logger;
            _mediator = mediator;
        }

        public async Task<BaseResponseDto<bool>> Handle(CreateOrderRequest request, CancellationToken cancellationToken)
        {
            BaseResponseDto<bool> response = new BaseResponseDto<bool>();

            try
            {
                var order = new Order
                {
                    ProductCode = request.ProductCode,
                    Price = request.Price,
                    Quantity = request.Quantity
                };

                await _repository.CreateAsync(order);

                response.Data = true;

                await _mediator.Publish(new OrderCreatedEvent(productCode: order.ProductCode, quantity: order.Quantity, price: order.Price));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                response.Errors.Add("An error occurred while creating the order.");
            }

            return response;
        }
    }
}
