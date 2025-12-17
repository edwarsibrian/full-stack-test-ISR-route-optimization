using ISR.Application.Routes;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ISR.Api.Controllers
{
    [Route("api/routes")]
    [ApiController]
    public class RoutesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RoutesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<OptimizedRouteResult>> GetRoute()
        {
            var result = await _mediator.Send(new GetOptimizedRouteQuery());
            return Ok(result);
        }
    }
}
