using MediatR;

namespace Campaign.Core.Dtos.Requests
{
    public class CreateProductRequest : IRequest<BaseResponseDto<bool>>
    {
        public string ProductCode { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
    }
}
 