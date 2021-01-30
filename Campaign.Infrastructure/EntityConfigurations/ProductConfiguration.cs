using Campaign.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Campaign.Infrastructure.EntityConfigurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(_ => _.Id); 
        }
    }
}
