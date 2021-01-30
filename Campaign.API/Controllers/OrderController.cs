using Campaign.Core.Dtos;
using Campaign.Core.Dtos.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Campaign.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrdersController(IMediator mediator)
        {
            _mediator = mediator;
        } 

        [HttpPost]
        public async Task<ActionResult<string>> CreateOrderAsync([FromBody] CreateOrderRequest createOrderRequest)
        {
            BaseResponseDto<bool> createResponse = await _mediator.Send(createOrderRequest);

            if (createResponse.Data)
            {
                return Created("...", null);
            }
            else
            {
                return BadRequest(createResponse.Errors);
            }
        }

        [HttpGet("productName")]
        public async Task<ActionResult<List<ProductDto>>> GetOrderAsync(string productName)
        {
            BaseResponseDto<List<OrderDto>> getOrderReponse = await _mediator.Send(new GetOrderRequest(productName));

            if (!getOrderReponse.HasError)
            {
                return Ok(getOrderReponse.Data);
            }
            else
            {
                return BadRequest(getOrderReponse.Errors);
            }
        }
    }
}
