using ISR.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace ISR.Application.Leads.Upload
{
    public sealed record UploadLeadsCommand(
    IFormFile ManagerFile,
    IFormFile? IsrFile
) : IRequest<UploadLeadsResult>;
}
