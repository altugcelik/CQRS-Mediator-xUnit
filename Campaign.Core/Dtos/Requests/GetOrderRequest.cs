using MediatR;
using System;
using System.Collections.Generic;

namespace Campaign.Core.Dtos.Requests
{
   public class GetOrderRequest : IRequest<BaseResponseDto<List<OrderDto>>>
    {
        public GetOrderRequest(string productName)
        {
            this.productName = productName;
        }

        public string productName { get; set; }
    }
}
 