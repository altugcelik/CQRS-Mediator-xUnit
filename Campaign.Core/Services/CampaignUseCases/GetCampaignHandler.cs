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

namespace Campaign.Core.Services.CampaignUseCases
{
    public class GetCampaignHandler : IRequestHandler<GetCampaignRequest, BaseResponseDto<List<CampaignDto>>>
    {
        private readonly IRepository<Campaigning> _repository;
        private readonly ILogger<GetCampaignHandler> _logger; 

        public GetCampaignHandler(IRepository<Campaigning> repository, ILogger<GetCampaignHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<BaseResponseDto<List<CampaignDto>>> Handle(GetCampaignRequest request, CancellationToken cancellationToken)
        {
            BaseResponseDto<List<CampaignDto>> response = new BaseResponseDto<List<CampaignDto>>();

            try
            {
                List<CampaignDto> campaign = (await _repository.GetWhereAsync(c => c.CampaignName == request.campaignName)).Select(c => new CampaignDto
                {
                    Duration = c.Duration,
                    Limit = c.Limit,
                    CampaignName = c.CampaignName,
                    ProductCode = c.ProductCode,
                    TargetSalesCount = c.TargetSalesCount
                }).ToList();

                response.Data = campaign;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                response.Errors.Add("An error occurred while getting campaign.");
            }

            return response;
        }
    }
}
