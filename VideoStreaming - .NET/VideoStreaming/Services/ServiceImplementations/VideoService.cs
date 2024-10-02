using VideoStreaming.Entities;
using VideoStreaming.Models.Requests;
using VideoStreaming.Repositories.RepositoryInterfaces;
using VideoStreaming.Services.ServiceInterfaces;

namespace VideoStreaming.Services.ServiceImplementations
{
    public class VideoService : IVideoService
    {
        private readonly IWebHostEnvironment _hostingEnv;
        private IVideoRepository _videoRepository;
        public VideoService(IWebHostEnvironment hostingEnv, IVideoRepository videoRepository) {
            _hostingEnv = hostingEnv;
            _videoRepository = videoRepository;
        }

        public async Task UploadVideo(VideoUploadRequest uploadReq)
        {
            if (uploadReq.VideoFile == null || uploadReq.VideoFile.Length == 0)
            {
                throw new Exception("No file uploaded.");  
            }
            if (!uploadReq.VideoFile.ContentType.StartsWith("video/")) throw new Exception("Only video file is supported");
            
            string folderName = $"uploads\\Videos";
            string webRootPath = _hostingEnv.ContentRootPath;
            string newPath;
            try
            {
                newPath = Path.Combine(webRootPath, folderName);
            }
            catch (Exception)
            {
                newPath = Path.Combine(_hostingEnv.ContentRootPath, folderName);
            }

            if (!Directory.Exists(newPath)) Directory.CreateDirectory(newPath);

            var filePath = Path.Combine(newPath, uploadReq.VideoFile.FileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await uploadReq.VideoFile.CopyToAsync(stream);
            }
            Video video = new Video()
            {
                FileName = uploadReq.VideoFile.FileName,
                UploadDate = DateTime.UtcNow,
            };
            await _videoRepository.AddAsync(video);
        }

        public async Task<List<Video>> GetVideoList()
        {
            return (await _videoRepository.GetAllAsync()).ToList();
        }
    }
}
