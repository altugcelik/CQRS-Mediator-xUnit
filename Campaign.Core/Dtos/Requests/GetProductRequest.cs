using MediatR;
using System.Collections.Generic;

namespace Campaign.Core.Dtos.Requests
{
    public class GetProductRequest : IRequest<BaseResponseDto<ProductDto>>
    {
        public GetProductRequest(string productCode)
        {
            this.productCode = productCode;
        }

        public string productCode { get; set; }
    }
}
 