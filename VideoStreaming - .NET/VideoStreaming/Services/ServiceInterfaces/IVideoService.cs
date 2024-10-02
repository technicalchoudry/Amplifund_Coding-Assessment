using VideoStreaming.Entities;
using VideoStreaming.Models.Requests;

namespace VideoStreaming.Services.ServiceInterfaces
{
    public interface IVideoService
    {
        Task UploadVideo(VideoUploadRequest uploadRequest);
        Task<List<Video>> GetVideoList();
    }
}
