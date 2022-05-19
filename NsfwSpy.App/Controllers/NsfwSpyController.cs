using HeyRed.Mime;
using Microsoft.AspNetCore.Mvc;
using System.Web;

namespace NsfwSpyNS.App.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NsfwSpyController : ControllerBase
    {
        private INsfwSpy _nsfwSpy;

        public NsfwSpyController(INsfwSpy nsfwSpy)
        {
            _nsfwSpy = nsfwSpy;
        }

        [HttpGet("url/{url}")]
        public async Task<FileContentResult> GetUrlMediaAsync(string url)
        {
            url = HttpUtility.UrlDecode(url);
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/101.0.4951.67 Safari/537.36");
            var byteArray = await httpClient.GetByteArrayAsync(url);
            var mimeType = MimeGuesser.GuessMimeType(byteArray);
            return new FileContentResult(byteArray, mimeType);
        }

        [HttpPost("image")]
        public ActionResult<NsfwSpyResult> ClassifyImage(IFormFile file)
        {
            var fileBytes = ConvertFormFileToByteArray(file);
            var result = _nsfwSpy.ClassifyImage(fileBytes);
            return Ok(result);
        }

        [HttpPost("gif")]
        public ActionResult<NsfwSpyFramesResult> ClassifyGif(IFormFile file)
        {
            var fileBytes = ConvertFormFileToByteArray(file);
            var videoOptions = new VideoOptions
            {
                EarlyStopOnNsfw = false
            };
            var result = _nsfwSpy.ClassifyGif(fileBytes, videoOptions);
            return Ok(result);
        }

        [HttpPost("video")]
        public ActionResult<NsfwSpyFramesResult> ClassifyVideo(IFormFile file)
        {
            var fileBytes = ConvertFormFileToByteArray(file);
            var videoOptions = new VideoOptions
            {
                EarlyStopOnNsfw = false
            };
            var result = _nsfwSpy.ClassifyVideo(fileBytes, videoOptions);
            return Ok(result);
        }

        private byte[] ConvertFormFileToByteArray(IFormFile file)
        {
            using (var ms = new MemoryStream())
            {
                file.CopyTo(ms);
                var fileBytes = ms.ToArray();
                return fileBytes;
            }
        }
    }

    public class MediaInfo
    {
        public IFormFile File { get; set; }
        public string MimeType { get; set; }
    }
}