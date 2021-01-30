using Campaign.Core.Dtos;
using Campaign.Core.Dtos.Requests;
using Campaign.Core.Interfaces;
using Campaign.Core.Models;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Campaign.Core.Services.OrderUseCases
{
    public class GetOrderHandler : IRequestHandler<GetOrderRequest, BaseResponseDto<List<OrderDto>>>
    {
        private readonly IRepository<Order> _repository;
        private readonly ILogger<GetOrderHandler> _logger;

        public GetOrderHandler(IRepository<Order> repository, ILogger<GetOrderHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<BaseResponseDto<List<OrderDto>>> Handle(GetOrderRequest request, CancellationToken cancellationToken)
        {
            BaseResponseDto<List<OrderDto>> response = new BaseResponseDto<List<OrderDto>>();

            try
            {
                List<OrderDto> order = (await _repository.GetWhereAsync(o => o.ProductCode == request.productName)).Select(o => new OrderDto
                {
                    Price = o.Price,
                    ProductCode = o.ProductCode,
                    Quantity = o.Quantity
                }).ToList();

                response.Data = order;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                response.Errors.Add("An error occurred while getting order.");
            }

            return response;
        }
    }
}
