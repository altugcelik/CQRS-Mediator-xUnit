using Campaign.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Campaign.Infrastructure.EntityConfigurations
{
    public class OrderConfigurations : IEntityTypeConfiguration<Order>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(_ => _.Id);
        }
    } 
}
