﻿namespace Campaign.Core.Models
{
    public class Campaigning : EntityBase
    {
        public string CampaignName { get; set; }
        public string ProductCode { get; set; }
        public int Duration { get; set; }
        public int Limit { get; set; }
        public int TargetSalesCount { get; set; }
    }
}
 