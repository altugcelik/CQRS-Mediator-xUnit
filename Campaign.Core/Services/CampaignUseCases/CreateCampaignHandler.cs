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

namespace Campaign.Core.Services.CampaignUseCases
{
    public class CreateCampaignHandler : IRequestHandler<CreateCampaignRequest, BaseResponseDto<bool>>
    {
        private readonly IRepository<Campaigning> _repository;
        private readonly ILogger<CreateCampaignHandler> _logger;
        private readonly IMediator _mediator;

        public CreateCampaignHandler(IRepository<Campaigning> repository, ILogger<CreateCampaignHandler> logger, IMediator mediator)
        {
            _repository = repository;
            _logger = logger;
            _mediator = mediator;
        }

        public async Task<BaseResponseDto<bool>> Handle(CreateCampaignRequest request, CancellationToken cancellationToken)
        {
            BaseResponseDto<bool> response = new BaseResponseDto<bool>();

            try
            {
                var campaign = new Campaigning
                {
                    Duration = request.Duration,
                    CreatedAt = DateTime.Now,
                    Limit = request.Limit,
                    CampaignName = request.CampaignName,
                    ProductCode = request.ProductCode,
                    TargetSalesCount = request.TargetSalesCount
                };

                await _repository.CreateAsync(campaign);

                response.Data = true;

                await _mediator.Publish(new CampaignCreatedEvent(campaignName: campaign.CampaignName));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                response.Errors.Add("An error occurred while creating the campaign.");
            }

            return response;
        }
    }
}