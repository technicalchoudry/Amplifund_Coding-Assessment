using System.ComponentModel.DataAnnotations;

namespace VideoStreaming.Entities
{
    public class Video
    {
        [Key]
        public int VideoId { get; set; }

        public string FileName { get; set; }

        public DateTime UploadDate { get; set; }
    }
}
