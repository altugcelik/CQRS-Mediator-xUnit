using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Campaign.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IncreaseTimesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public IncreaseTimesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public IActionResult GetIncreaseTimeAsync()
        {
            Core.Helper.SystemDateHelper.Time = Core.Helper.SystemDateHelper.Time == 23 ? 0 : Core.Helper.SystemDateHelper.Time + 1;

            return Ok($"Time is {Core.Helper.SystemDateHelper.Time}");

        }
    }
}
