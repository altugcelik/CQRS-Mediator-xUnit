using MediatR;

namespace Campaign.Core.Dtos.Requests
{
    public class CreateOrderRequest : IRequest<BaseResponseDto<bool>>
    {
        public string ProductCode { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
 