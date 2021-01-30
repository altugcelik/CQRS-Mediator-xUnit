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
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<string>> CreateProductAsync([FromBody] CreateProductRequest createProductRequest)
        {
            BaseResponseDto<bool> createResponse = await _mediator.Send(createProductRequest);

            if (createResponse.Data)
            {
                return Created("...", null); 
            }
            else
            {
                return BadRequest(createResponse.Errors);
            }
        }

        [HttpGet("productCode")]
        public async Task<ActionResult<List<ProductDto>>> GetProductAsync(string productCode)
        {
            BaseResponseDto<ProductDto> getProductReponse = await _mediator.Send(new GetProductRequest(productCode));

            if (!getProductReponse.HasError)
            {
                return Ok(getProductReponse.Data);
            }
            else
            {
                return BadRequest(getProductReponse.Errors);
            }
        }
    }
}
