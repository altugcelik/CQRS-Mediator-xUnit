using MediatR;

namespace Campaign.Core.Dtos.Requests
{
    public class CreateCampaignRequest : IRequest<BaseResponseDto<bool>>
    {
        public string CampaignName { get; set; }
        public string ProductCode { get; set; }
        public int Duration { get; set; }
        public int Limit { get; set; } 
        public int TargetSalesCount { get; set; }
    }
}
