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
namespace Campaign.Core.Services.CampaignUseCases
{
    public class GetCampaignByProductCodeHandler : IRequestHandler<GetCampaignByProductCodeRequest, BaseResponseDto<CampaignDto>>
    {
        private readonly IRepository<Campaigning> _repository;
        private readonly ILogger<GetCampaignByProductCodeHandler> _logger;

        public GetCampaignByProductCodeHandler(IRepository<Campaigning> repository, ILogger<GetCampaignByProductCodeHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<BaseResponseDto<CampaignDto>> Handle(GetCampaignByProductCodeRequest request, CancellationToken cancellationToken)
        {
            BaseResponseDto<CampaignDto> response = new BaseResponseDto<CampaignDto>(); 

            try
            {
                CampaignDto campaign = (await _repository.GetWhereAsync(c => c.ProductCode == request.ProductCode && c.Duration >= Helper.SystemDateHelper.Time)).Select(c => new CampaignDto
                {
                    Duration = c.Duration,
                    Limit = c.Limit,
                    CampaignName = c.CampaignName,
                    ProductCode = c.ProductCode,
                    TargetSalesCount = c.TargetSalesCount
                }).FirstOrDefault();

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