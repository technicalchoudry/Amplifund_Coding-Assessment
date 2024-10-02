using Microsoft.AspNetCore.Mvc;
using VideoStreaming.Models;
using VideoStreaming.Models.Requests;
using VideoStreaming.Services.ServiceInterfaces;

namespace VideoStreaming.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VideoController : ControllerBase
    {
        private IVideoService _videoService;
        private IWebHostEnvironment _env;

        public VideoController(IVideoService videoService, IWebHostEnvironment env)
        {
            _videoService = videoService;
            _env = env;
        }

        [HttpPost]
        [Route("upload")]
        public async Task<IActionResult> Upload([FromForm] VideoUploadRequest uploadReq)
        {
            Response result = new Response();
            try
            {
                await _videoService.UploadVideo(uploadReq);
                result.Status = Status.Success;
            }
            catch (Exception ex)
            {
                result.Status = Status.BadRequest;
                result.Message = "Something went wrong!";
                result.Error = ex.Message;

                Console.WriteLine(ex.ToString());
            }
            return Ok(result);
        }
        
        [HttpGet]
        [Route("list")]
        public async Task<IActionResult> GetVideoList()
        {
            Response result = new Response();
            try
            {
                var list = await _videoService.GetVideoList();
                result.Status = Status.Success;
                result.Message = $"{list.Count} Video(s) found";
                result.Data = list;

                if (list == null || list.Count <= 0)
                {
                    result.Status = Status.NoDataAvailable;
                    result.Message = "No video found";
                }
            }
            catch (Exception ex)
            {
                result.Status = Status.BadRequest;
                result.Message = "Something went wrong!";
                result.Error = ex.Message;

                Console.WriteLine(ex.ToString());
            }
            return Ok(result);
        }

        [HttpGet("stream/{videoName}")]
        public async Task<IActionResult> StreamVideo(string videoName)
        {
            string folderName = $"uploads\\Videos";
            string webRootPath = _env.ContentRootPath;
            string newPath;

            try
            {
                var videoPath = Path.Combine(webRootPath, folderName, videoName);

                if (!System.IO.File.Exists(videoPath))
                {
                    return NotFound();
                }

                var stream = new FileStream(videoPath, FileMode.Open, FileAccess.Read);
                var response = File(stream, "application/octet-stream", enableRangeProcessing: true);
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return BadRequest();
            }
        }
    }
}
