using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TravelWarrants.Interfaces;
using TravelWarrants.Services;

namespace TravelWarrants.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [RequestFormLimits(MultipartBodyLengthLimit = 512L * 1024 * 1024)]
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
