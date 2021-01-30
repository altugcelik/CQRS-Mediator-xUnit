using MediatR;

namespace Campaign.Core.Events
{
    public  class CampaignCreatedEvent : INotification
    {
        private readonly string _campaignName;

        public CampaignCreatedEvent(string campaignName)
        {
            _campaignName = campaignName;
        }
    }
}
 