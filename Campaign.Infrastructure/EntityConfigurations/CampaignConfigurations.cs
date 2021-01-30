using Campaign.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Campaign.Infrastructure.EntityConfigurations
{
    public class CampaignConfigurations : IEntityTypeConfiguration<Campaigning>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Campaigning> builder)
        {
            builder.HasKey(_ => _.Id); 
        }
    }
}
