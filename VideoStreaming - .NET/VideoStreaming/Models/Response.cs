using System.Net.NetworkInformation;

namespace VideoStreaming.Models
{
    public class Response
    {
        public Response()
        {
            Status = Status.Success;
            Message = Status.Success.ToString();
            Data = null;
            Error = null;
        }
        public Status Status { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
        public object Error { get; set; }
    }

}