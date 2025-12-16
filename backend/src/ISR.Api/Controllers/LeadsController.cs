using ISR.Api.DTOs;
using ISR.Application.Leads.Upload;
using ISR.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ISR.Api.Controllers
{
    [Route("api/leads")]
    [ApiController]
    public class LeadsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LeadsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("upload")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Upload(
            [FromForm] UploadLeadsRequest request,
            CancellationToken cancellationToken)
        {
            var command = new UploadLeadsCommand(request.ManagerFile, request.IsrFile);

            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }
    }
}
