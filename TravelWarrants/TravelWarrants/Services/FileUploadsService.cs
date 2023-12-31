﻿using TravelWarrants.DTOs;
using TravelWarrants.Interfaces;

namespace TravelWarrants.Services
{
    public class FileUploadsService : IFileUploadService
    {
        private readonly TravelWarrantsContext _context;
        private readonly string _folderPath;
        private readonly ILogger<FileUploadsService> _logger;
        public FileUploadsService(TravelWarrantsContext context, ILogger<FileUploadsService> logger)
        {
            _context = context;
            _folderPath = Path.Combine(Directory.GetCurrentDirectory(), "RoutePlan");
            _logger = logger;
        }

        public async Task<int> UploadFileBuffering(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                throw new ArgumentNullException(nameof(file));
            }

            if (!Directory.Exists(_folderPath))
            {
                Directory.CreateDirectory(_folderPath);
            }

            var date = DateTime.Now.ToString("yyyy_MM_dd-HH_mm_ss");
            var newFileName = $"{date}_{file.FileName}";
            var filePath = Path.Combine(_folderPath, newFileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var uploadedFile = new UploadedFiles
            {
                Path = filePath,
                FileName = newFileName,
                UploadDate = DateTime.UtcNow,
            };

            _context.UploadedFiles.Add(uploadedFile);
            await _context.SaveChangesAsync();

            return uploadedFile.Id;
        }

        public async Task<int> UploadFileStreaming(IFormFile file, CancellationToken cancellationToken = default)
        {
            if (file == null || file.Length == 0)
            {
                throw new ArgumentException("Invalid file.");
            }

            if (!Directory.Exists(_folderPath))
            {
                Directory.CreateDirectory(_folderPath);
            }

            var fileName = Path.GetFileNameWithoutExtension(file.FileName);
            var fileExtension = Path.GetExtension(file.FileName);
            var newFileName = fileName;
            int count = 1;

            while (File.Exists(Path.Combine(_folderPath, newFileName + fileExtension)))
            {
                newFileName = $"{fileName}({count})";
                count++;
            }
            newFileName += fileExtension;

            var filePath = Path.Combine(_folderPath, newFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                var buffer = new byte[1048576]; // 1 MB buffer
                int bytesRead;
                var fileReadStream = file.OpenReadStream();
                while ((bytesRead = await fileReadStream.ReadAsync(buffer, cancellationToken)) > 0)
                {
                    await fileStream.WriteAsync(buffer.AsMemory(0, bytesRead), cancellationToken);
                    _logger.LogInformation($"{bytesRead} bytes read and written to file");
                }
            }

            var uploadedFile = new UploadedFiles
            {
                Path = filePath,
                FileName = newFileName,
                UploadDate = DateTime.UtcNow
            };

            _context.UploadedFiles.Add(uploadedFile);
            await _context.SaveChangesAsync(cancellationToken);

            return uploadedFile.Id;
        }



        public async Task DeleteFile(int fileId)
        {
            var fileToDelete = await _context.UploadedFiles.FirstOrDefaultAsync(x => x.Id == fileId);
            if (fileToDelete == null)
            {
                return;
            }

            var filePath = fileToDelete.Path;
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            _context.UploadedFiles.Remove(fileToDelete);
            await _context.SaveChangesAsync();
        }

        public async Task<FileData> DownloadRoutePlan(int fileId)
        {
            var file = await _context.UploadedFiles.FirstOrDefaultAsync(x => x.Id == fileId)
                ?? throw new FileNotFoundException("File not found.");

            var path = file.Path;
            var fileName = Path.GetFileName(path);

            var memory = new MemoryStream();
            using (var stream = new FileStream(path, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;

            return new FileData { FileStream = memory, FileName = fileName };

        }
    }
}
