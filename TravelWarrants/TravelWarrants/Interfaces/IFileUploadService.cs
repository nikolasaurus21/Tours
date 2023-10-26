using TravelWarrants.DTOs;

namespace TravelWarrants.Interfaces
{
    public interface IFileUploadService
    {
        Task DeleteFile(int fileId);
        Task<FileData> DownloadRoutePlan(int fileId);
        Task<int> UploadFileBuffering(IFormFile file);
        Task<int> UploadFileStreaming(IFormFile file, CancellationToken cancellationToken = default);

    }
}
