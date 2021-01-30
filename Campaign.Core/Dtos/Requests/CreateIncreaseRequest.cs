using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Campaign.Core.Dtos.Requests
{
    public class CreateIncreaseRequest : IRequest<BaseResponseDto<bool>>
    {
        public int Time { get; set; } 
    }
}
