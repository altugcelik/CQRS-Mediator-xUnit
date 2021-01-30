using MediatR;
using System.Collections.Generic;

namespace Campaign.Core.Dtos.Requests
{
    public class GetCampaignRequest : IRequest<BaseResponseDto<List<CampaignDto>>>
    {
        public GetCampaignRequest(string campaignName)
        {
            this.campaignName = campaignName;
        }

        public string campaignName { get; set; }
    }
}
 