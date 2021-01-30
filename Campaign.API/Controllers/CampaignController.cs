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
    public class CampaignsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CampaignsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<string>> CreateOrderAsync([FromBody] CreateCampaignRequest createCampaignRequest)
        {
            BaseResponseDto<bool> createResponse = await _mediator.Send(createCampaignRequest);

            if (createResponse.Data)
            {
                return Created("...", null);
            }
            else
            {
                return BadRequest(createResponse.Errors);
            }
        }

        [HttpGet("campaignName")]
        public async Task<ActionResult<List<ProductDto>>> GetCampaignAsync(string campaignName)
        {
            BaseResponseDto<List<CampaignDto>> getCampaignReponse = await _mediator.Send(new GetCampaignRequest(campaignName));

            if (!getCampaignReponse.HasError)
            { 
                return Ok(getCampaignReponse.Data);
            }
            else
            {
                return BadRequest(getCampaignReponse.Errors);
            }
        }
    }
}
