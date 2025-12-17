using ISR.Application.Common.Interfaces;
using ISR.Application.Home;
using ISR.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ISR.Api.Controllers
{
    [Route("api/home-address")]
    [ApiController]
    public class HomeAddressController : ControllerBase
    {
        private readonly IMediator _mediator;

        public HomeAddressController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<HomeAddressResult>> Get(CancellationToken ct)
        {
            var result = await _mediator.Send(new GetHomeAddressQuery(), ct);

            if (result is null)
                return NoContent();

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<HomeAddressResult>> Set(
            [FromBody] SetHomeAddressCommand command,
            CancellationToken ct)
        {
            return Ok(await _mediator.Send(command, ct));
        }
    }
}
