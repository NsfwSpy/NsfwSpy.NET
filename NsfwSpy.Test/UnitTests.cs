using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace NsfwSpyNS.Test
{
    public class UnitTests
    {
        [Fact]
        public void ClassifyImageByteArray_ValidByteArray()
        {
            var filePath = Path.Combine(AppContext.BaseDirectory, @"Assets/flower.jpg");
            var imageBytes = File.ReadAllBytes(filePath);

            var nsfwSpy = new NsfwSpy();
            var result = nsfwSpy.ClassifyImage(imageBytes);

            Assert.Equal("Neutral", result.PredictedLabel);
        }

        [Fact]
        public void ClassifyImageUri_ValidUri()
        {
            var uri = new Uri("https://pbs.twimg.com/profile_images/883859744498176000/pjEHfbdn_400x400.jpg");

            var nsfwSpy = new NsfwSpy();
            var result = nsfwSpy.ClassifyImage(uri);

            Assert.Equal("Neutral", result.PredictedLabel);
        }

        [Fact]
        public void ClassifyImageUri_CustomWebClient()
        {
            var uri = new Uri("https://pbs.twimg.com/profile_images/883859744498176000/pjEHfbdn_400x400.jpg");
            var webClient = new WebClient();
            webClient.Headers["User-Agent"] = "test";

            var nsfwSpy = new NsfwSpy();
            var result = nsfwSpy.ClassifyImage(uri, webClient);

            Assert.Equal("Neutral", result.PredictedLabel);
        }

        [Fact]
        public void ClassifyImageFilePath_ValidFilePath()
        {
            var filePath = Path.Combine(AppContext.BaseDirectory, @"Assets/flower.jpg");

            var nsfwSpy = new NsfwSpy();
            var result = nsfwSpy.ClassifyImage(filePath);

            Assert.Equal("Neutral", result.PredictedLabel);
        }

        [Fact]
        public void ClassifyImageFilePath_InvalidFilePath()
        {
            var filePath = Path.Combine(AppContext.BaseDirectory, @"Assets/filedoesnotexist.jpg");

            var nsfwSpy = new NsfwSpy();
            Assert.Throws<FileNotFoundException>(() => nsfwSpy.ClassifyImage(filePath));
        }

        [Fact]
        public async Task ClassifyImageUriAsync_ValidUri()
        {
            var uri = new Uri("https://pbs.twimg.com/profile_images/883859744498176000/pjEHfbdn_400x400.jpg");

            var nsfwSpy = new NsfwSpy();
            var result = await nsfwSpy.ClassifyImageAsync(uri);

            Assert.Equal("Neutral", result.PredictedLabel);
        }

        [Fact]
        public async Task ClassifyImageUriAsync_CustomWebClient()
        {
            var uri = new Uri("https://pbs.twimg.com/profile_images/883859744498176000/pjEHfbdn_400x400.jpg");
            var webClient = new WebClient();
            webClient.Headers["User-Agent"] = "test";

            var nsfwSpy = new NsfwSpy();
            var result = await nsfwSpy.ClassifyImageAsync(uri, webClient);

            Assert.Equal("Neutral", result.PredictedLabel);
        }

        [Fact]
        public async Task ClassifyImageFilePathAsync_ValidFilePath()
        {
            var filePath = Path.Combine(AppContext.BaseDirectory, @"Assets/flower.jpg");

            var nsfwSpy = new NsfwSpy();
            var result = await nsfwSpy.ClassifyImageAsync(filePath);

            Assert.Equal("Neutral", result.PredictedLabel);
        }

        [Fact]
        public void ClassifyGifByteArray_ValidByteArray()
        {
            var filePath = Path.Combine(AppContext.BaseDirectory, @"Assets/cool.gif");
            var imageBytes = File.ReadAllBytes(filePath);

            var nsfwSpy = new NsfwSpy();
            var result = nsfwSpy.ClassifyGif(imageBytes);

            Assert.Equal(10, result.Frames.Count);
            Assert.False(result.IsNsfw);
        }

        [Fact]
        public void ClassifyGifFilePath_ValidFilePath()
        {
            var filePath = Path.Combine(AppContext.BaseDirectory, @"Assets/cool.gif");

            var nsfwSpy = new NsfwSpy();
            var result = nsfwSpy.ClassifyGif(filePath);

            Assert.Equal(10, result.Frames.Count);
            Assert.False(result.IsNsfw);
        }

        [Fact]
        public void ClassifyGifFilePath_ClassifyEvery2ndFrame()
        {
            var filePath = Path.Combine(AppContext.BaseDirectory, @"Assets/cool.gif");
            var videoOptions = new VideoOptions
            {
                ClassifyEveryNthFrame = 2
            };

            var nsfwSpy = new NsfwSpy();
            var result = nsfwSpy.ClassifyGif(filePath, videoOptions);

            Assert.Equal(5, result.Frames.Count);
            Assert.False(result.IsNsfw);
        }

        [Fact]
        public void ClassifyGifFilePath_EndEarlyOnNsfw()
        {
            var filePath = Path.Combine(AppContext.BaseDirectory, @"Assets/bikini.gif");
            var videoOptions = new VideoOptions
            {
                EarlyStopOnNsfw = true
            };

            var nsfwSpy = new NsfwSpy();
            var result = nsfwSpy.ClassifyGif(filePath, videoOptions: videoOptions);

            Assert.True(result.IsNsfw);
            Assert.True(result.Frames.Count < 181); // This Gif has 181 frames
        }

        [Fact]
        public void ClassifyGifUri_ValidUri()
        {
            var uri = new Uri("https://media2.giphy.com/media/62PP2yEIAZF6g/giphy.gif");

            var nsfwSpy = new NsfwSpy();
            var result = nsfwSpy.ClassifyGif(uri);

            Assert.Equal(10, result.Frames.Count);
            Assert.False(result.IsNsfw);
        }

        [Fact]
        public void ClassifyGifUri_ClassifyEvery2ndFrame()
        {
            var uri = new Uri("https://media2.giphy.com/media/62PP2yEIAZF6g/giphy.gif");
            var videoOptions = new VideoOptions
            {
                ClassifyEveryNthFrame = 2
            };

            var nsfwSpy = new NsfwSpy();
            var result = nsfwSpy.ClassifyGif(uri, videoOptions: videoOptions);

            Assert.Equal(5, result.Frames.Count);
            Assert.False(result.IsNsfw);
        }

        [Fact]
        public void ClassifyGifUri_EndEarlyOnNsfw()
        {
            var uri = new Uri("https://c.tenor.com/5y-jOowm51MAAAAd/bikini.gif");
            var videoOptions = new VideoOptions
            {
                EarlyStopOnNsfw = true
            };

            var nsfwSpy = new NsfwSpy();
            var result = nsfwSpy.ClassifyGif(uri, videoOptions: videoOptions);

            Assert.True(result.IsNsfw);
            Assert.True(result.Frames.Count < 181); // This Gif has 181 frames
        }

        [Fact]
        public async Task ClassifyGifFilePathAsync_ValidFilePath()
        {
            var filePath = Path.Combine(AppContext.BaseDirectory, @"Assets/cool.gif");

            var nsfwSpy = new NsfwSpy();
            var result = await nsfwSpy.ClassifyGifAsync(filePath);

            Assert.Equal(10, result.Frames.Count);
            Assert.False(result.IsNsfw);
        }

        [Fact]
        public async Task ClassifyGifUriAsync_ValidUri()
        {
            var uri = new Uri("https://media2.giphy.com/media/62PP2yEIAZF6g/giphy.gif");

            var nsfwSpy = new NsfwSpy();
            var result = await nsfwSpy.ClassifyGifAsync(uri);

            Assert.Equal(10, result.Frames.Count);
            Assert.False(result.IsNsfw);
        }

        [Fact]
        public void ClassifyMp4ByteArray_ValidByteArray()
        {
            var filePath = Path.Combine(AppContext.BaseDirectory, @"Assets\bikini.mp4");
            var imageBytes = File.ReadAllBytes(filePath);

            var nsfwSpy = new NsfwSpy();
            var result = nsfwSpy.ClassifyMp4(imageBytes);

            Assert.Equal(181, result.Frames.Count);
            Assert.True(result.IsNsfw);
        }

        [Fact]
        public void ClassifyMp4FilePath_ValidFilePath()
        {
            var filePath = Path.Combine(AppContext.BaseDirectory, @"Assets\bikini.mp4");

            var nsfwSpy = new NsfwSpy();
            var result = nsfwSpy.ClassifyMp4(filePath);

            Assert.Equal(181, result.Frames.Count);
            Assert.True(result.IsNsfw);
        }

        [Fact]
        public void ClassifyMp4FilePath_ClassifyEvery2ndFrame()
        {
            var filePath = Path.Combine(AppContext.BaseDirectory, @"Assets\bikini.mp4");
            var videoOptions = new VideoOptions
            {
                ClassifyEveryNthFrame = 2
            };

            var nsfwSpy = new NsfwSpy();
            var result = nsfwSpy.ClassifyMp4(filePath, videoOptions);

            Assert.Equal(91, result.Frames.Count);
            Assert.True(result.IsNsfw);
        }

        [Fact]
        public void ClassifyMp4FilePath_EndEarlyOnNsfw()
        {
            var filePath = Path.Combine(AppContext.BaseDirectory, @"Assets\bikini.mp4");
            var videoOptions = new VideoOptions
            {
                EarlyStopOnNsfw = true
            };

            var nsfwSpy = new NsfwSpy();
            var result = nsfwSpy.ClassifyMp4(filePath, videoOptions: videoOptions);

            Assert.True(result.IsNsfw);
            Assert.True(result.Frames.Count < 181); // This video has 181 frames
        }

        [Fact]
        public void ClassifyMp4Uri_ValidUri()
        {
            var uri = new Uri("https://i.imgur.com/MjTH5ZS.mp4");

            var nsfwSpy = new NsfwSpy();
            var result = nsfwSpy.ClassifyMp4(uri);

            Assert.Equal(120, result.Frames.Count);
            Assert.True(result.IsNsfw);
        }

        [Fact]
        public void ClassifyMp4Uri_ClassifyEvery2ndFrame()
        {
            var uri = new Uri("https://i.imgur.com/MjTH5ZS.mp4");
            var videoOptions = new VideoOptions
            {
                ClassifyEveryNthFrame = 2
            };

            var nsfwSpy = new NsfwSpy();
            var result = nsfwSpy.ClassifyMp4(uri, videoOptions: videoOptions);

            Assert.Equal(60, result.Frames.Count);
            Assert.True(result.IsNsfw);
        }

        [Fact]
        public void ClassifyMp4Uri_EndEarlyOnNsfw()
        {
            var uri = new Uri("https://i.imgur.com/MjTH5ZS.mp4");
            var videoOptions = new VideoOptions
            {
                EarlyStopOnNsfw = true
            };

            var nsfwSpy = new NsfwSpy();
            var result = nsfwSpy.ClassifyMp4(uri, videoOptions: videoOptions);

            Assert.True(result.IsNsfw);
            Assert.True(result.Frames.Count < 120); // This video has 120 frames
        }

        [Fact]
        public async Task ClassifyMp4FilePathAsync_ValidFilePath()
        {
            var filePath = Path.Combine(AppContext.BaseDirectory, @"Assets\bikini.mp4");

            var nsfwSpy = new NsfwSpy();
            var result = await nsfwSpy.ClassifyMp4Async(filePath);

            Assert.Equal(181, result.Frames.Count);
            Assert.True(result.IsNsfw);
        }

        [Fact]
        public async Task ClassifyMp4UriAsync_ValidUri()
        {
            var uri = new Uri("https://i.imgur.com/MjTH5ZS.mp4");

            var nsfwSpy = new NsfwSpy();
            var result = await nsfwSpy.ClassifyMp4Async(uri);

            Assert.Equal(120, result.Frames.Count);
            Assert.True(result.IsNsfw);
        }
    }
}
