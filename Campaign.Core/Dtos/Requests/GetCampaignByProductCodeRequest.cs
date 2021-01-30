using MediatR;

namespace Campaign.Core.Dtos.Requests
{
    public class GetCampaignByProductCodeRequest : IRequest<BaseResponseDto<CampaignDto>>
    {
        public GetCampaignByProductCodeRequest(string productCode)
        {
            this.ProductCode = productCode;
        }

        public string ProductCode { get; private set; }
    }
}
 