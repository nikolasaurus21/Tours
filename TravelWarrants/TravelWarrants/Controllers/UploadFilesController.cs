using Microsoft.AspNetCore.Mvc;
using TravelWarrants.Interfaces;

namespace TravelWarrants.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [RequestSizeLimit(100_000_000_000)]
    public class UploadFilesController : ControllerBase
    {
        private readonly IFileUploadService _fileService;

        public UploadFilesController(IFileUploadService fileService)
        {
            _fileService = fileService;
        }

        [HttpPost("buffering-upload")]
        public async Task<ActionResult> UploadFileBuffering(IFormFile file)
        {
            if (file == null)
            {
                throw new ArgumentNullException(nameof(file));
            }

            var result = await _fileService.UploadFileBuffering(file);
            return Ok(result);
        }

        [HttpPost("streaming-upload")]
        public async Task<ActionResult> UploadFileStreaming(IFormFile file)
        {
            if (file == null)
            {
                throw new ArgumentNullException(nameof(file));
            }

            var result = await _fileService.UploadFileStreaming(file);
            return Ok(result);
        }


    }
}
