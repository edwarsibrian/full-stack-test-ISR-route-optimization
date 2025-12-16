using Microsoft.AspNetCore.Mvc;

namespace ISR.Api.DTOs
{
    public sealed class UploadLeadsRequest
    {
        [FromForm(Name = "managerFile")]
        public IFormFile ManagerFile { get; set; } = default!;

        [FromForm(Name = "isrFile")]
        public IFormFile? IsrFile { get; set; }
    }
}
