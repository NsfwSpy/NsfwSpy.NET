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
        [Theory]
        [InlineData("flower.jpg")]
        [InlineData("flower.png")]
        [InlineData("flower.webp")]
        public void ClassifyImageByteArray_ValidByteArray(string filename)
        {
            var filePath = Path.Combine(AppContext.BaseDirectory, $@"Assets/{filename}");
            var imageBytes = File.ReadAllBytes(filePath);

            var nsfwSpy = new NsfwSpy();
            var result = nsfwSpy.ClassifyImage(imageBytes);

            Assert.Equal("Neutral", result.PredictedLabel);
        }

        [Fact]
        public void ClassifyImageByteArray_InvalidByteArray()
        {
            var nsfwSpy = new NsfwSpy();
            Assert.Throws<ClassificationFailedException>(() => nsfwSpy.ClassifyImage(new byte[0]));
        }

        [Theory]
        [InlineData("https://raw.githubusercontent.com/d00ML0rDz/NsfwSpy/main/NsfwSpy.Test/Assets/flower.jpg")]
        [InlineData("https://raw.githubusercontent.com/d00ML0rDz/NsfwSpy/main/NsfwSpy.Test/Assets/flower.png")]
        [InlineData("https://raw.githubusercontent.com/d00ML0rDz/NsfwSpy/main/NsfwSpy.Test/Assets/flower.webp")]
        public void ClassifyImageUri_ValidUri(string url)
        {
            var uri = new Uri(url);

            var nsfwSpy = new NsfwSpy();
            var result = nsfwSpy.ClassifyImage(uri);

            Assert.Equal("Neutral", result.PredictedLabel);
        }

        [Theory]
        [InlineData("https://raw.githubusercontent.com/d00ML0rDz/NsfwSpy/main/NsfwSpy.Test/Assets/flower.jpg")]
        [InlineData("https://raw.githubusercontent.com/d00ML0rDz/NsfwSpy/main/NsfwSpy.Test/Assets/flower.png")]
        [InlineData("https://raw.githubusercontent.com/d00ML0rDz/NsfwSpy/main/NsfwSpy.Test/Assets/flower.webp")]
        public void ClassifyImageUri_CustomWebClient(string url)
        {
            var uri = new Uri(url);
            var webClient = new WebClient();
            webClient.Headers["User-Agent"] = "test";

            var nsfwSpy = new NsfwSpy();
            var result = nsfwSpy.ClassifyImage(uri, webClient);

            Assert.Equal("Neutral", result.PredictedLabel);
        }

        [Theory]
        [InlineData("flower.jpg")]
        [InlineData("flower.png")]
        [InlineData("flower.webp")]
        public void ClassifyImageFilePath_ValidFilePath(string filename)
        {
            var filePath = Path.Combine(AppContext.BaseDirectory, $@"Assets/{filename}");

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

        [Theory]
        [InlineData("https://raw.githubusercontent.com/d00ML0rDz/NsfwSpy/main/NsfwSpy.Test/Assets/flower.jpg")]
        [InlineData("https://raw.githubusercontent.com/d00ML0rDz/NsfwSpy/main/NsfwSpy.Test/Assets/flower.png")]
        [InlineData("https://raw.githubusercontent.com/d00ML0rDz/NsfwSpy/main/NsfwSpy.Test/Assets/flower.webp")]
        public async Task ClassifyImageUriAsync_ValidUri(string url)
        {
            var uri = new Uri(url);

            var nsfwSpy = new NsfwSpy();
            var result = await nsfwSpy.ClassifyImageAsync(uri);

            Assert.Equal("Neutral", result.PredictedLabel);
        }

        [Theory]
        [InlineData("https://raw.githubusercontent.com/d00ML0rDz/NsfwSpy/main/NsfwSpy.Test/Assets/flower.jpg")]
        [InlineData("https://raw.githubusercontent.com/d00ML0rDz/NsfwSpy/main/NsfwSpy.Test/Assets/flower.png")]
        [InlineData("https://raw.githubusercontent.com/d00ML0rDz/NsfwSpy/main/NsfwSpy.Test/Assets/flower.webp")]
        public async Task ClassifyImageUriAsync_CustomWebClient(string url)
        {
            var uri = new Uri(url);
            var webClient = new WebClient();
            webClient.Headers["User-Agent"] = "test";

            var nsfwSpy = new NsfwSpy();
            var result = await nsfwSpy.ClassifyImageAsync(uri, webClient);

            Assert.Equal("Neutral", result.PredictedLabel);
        }

        [Theory]
        [InlineData("flower.jpg")]
        [InlineData("flower.png")]
        [InlineData("flower.webp")]
        public async Task ClassifyImageFilePathAsync_ValidFilePath(string filename)
        {
            var filePath = Path.Combine(AppContext.BaseDirectory, $@"Assets/{filename}");

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
            var uri = new Uri("https://c.tenor.com/Rldfm57e8J4AAAAd/tenor.gif");
            var videoOptions = new VideoOptions
            {
                EarlyStopOnNsfw = true
            };

            var nsfwSpy = new NsfwSpy();
            var result = nsfwSpy.ClassifyGif(uri, videoOptions: videoOptions);

            Assert.True(result.IsNsfw);
            Assert.True(result.Frames.Count < 76); // This Gif has 76 frames
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

        [Theory]
        [InlineData("bikini.avi")]
        [InlineData("bikini.mkv")]
        [InlineData("bikini.mp4")]
        [InlineData("bikini.webm")]
        public void ClassifyVideoByteArray_ValidByteArray(string filename)
        {
            var filePath = Path.Combine(AppContext.BaseDirectory, $@"Assets\{filename}");
            var imageBytes = File.ReadAllBytes(filePath);

            var nsfwSpy = new NsfwSpy();
            var result = nsfwSpy.ClassifyVideo(imageBytes);

            if (filename.EndsWith(".webm"))
                Assert.Equal(180, result.Frames.Count);
            else
                Assert.Equal(181, result.Frames.Count);

            Assert.True(result.IsNsfw);
        }

        [Theory]
        [InlineData("bikini.avi")]
        [InlineData("bikini.mkv")]
        [InlineData("bikini.mp4")]
        [InlineData("bikini.webm")]
        public void ClassifyVideoFilePath_ValidFilePath(string filename)
        {
            var filePath = Path.Combine(AppContext.BaseDirectory, $@"Assets\{filename}");

            var nsfwSpy = new NsfwSpy();
            var result = nsfwSpy.ClassifyVideo(filePath);

            if (filename.EndsWith(".webm"))
                Assert.Equal(180, result.Frames.Count);
            else
                Assert.Equal(181, result.Frames.Count);

            Assert.True(result.IsNsfw);
        }

        [Theory]
        [InlineData("bikini.avi")]
        [InlineData("bikini.mkv")]
        [InlineData("bikini.mp4")]
        [InlineData("bikini.webm")]
        public void ClassifyVideoFilePath_ClassifyEvery2ndFrame(string filename)
        {
            var filePath = Path.Combine(AppContext.BaseDirectory, $@"Assets\{filename}");
            var videoOptions = new VideoOptions
            {
                ClassifyEveryNthFrame = 2
            };

            var nsfwSpy = new NsfwSpy();
            var result = nsfwSpy.ClassifyVideo(filePath, videoOptions);

            if (filename.EndsWith(".webm"))
                Assert.Equal(90, result.Frames.Count);
            else
                Assert.Equal(91, result.Frames.Count);

            Assert.True(result.IsNsfw);
        }

        [Theory]
        [InlineData("bikini.avi")]
        [InlineData("bikini.mkv")]
        [InlineData("bikini.mp4")]
        [InlineData("bikini.webm")]
        public void ClassifyVideoFilePath_EndEarlyOnNsfw(string filename)
        {
            var filePath = Path.Combine(AppContext.BaseDirectory, $@"Assets\{filename}");
            var videoOptions = new VideoOptions
            {
                EarlyStopOnNsfw = true
            };

            var nsfwSpy = new NsfwSpy();
            var result = nsfwSpy.ClassifyVideo(filePath, videoOptions: videoOptions);

            Assert.True(result.IsNsfw);
            Assert.True(result.Frames.Count < 181); // This video has 181 frames
        }

        [Fact]
        public void ClassifyVideoUri_ValidUri()
        {
            var uri = new Uri("https://i.imgur.com/MjTH5ZS.mp4");

            var nsfwSpy = new NsfwSpy();
            var result = nsfwSpy.ClassifyVideo(uri);

            Assert.Equal(120, result.Frames.Count);
            Assert.True(result.IsNsfw);
        }

        [Fact]
        public void ClassifyVideoUri_ClassifyEvery2ndFrame()
        {
            var uri = new Uri("https://i.imgur.com/MjTH5ZS.mp4");
            var videoOptions = new VideoOptions
            {
                ClassifyEveryNthFrame = 2
            };

            var nsfwSpy = new NsfwSpy();
            var result = nsfwSpy.ClassifyVideo(uri, videoOptions: videoOptions);

            Assert.Equal(60, result.Frames.Count);
            Assert.True(result.IsNsfw);
        }

        [Fact]
        public void ClassifyVideoUri_EndEarlyOnNsfw()
        {
            var uri = new Uri("https://i.imgur.com/MjTH5ZS.mp4");
            var videoOptions = new VideoOptions
            {
                EarlyStopOnNsfw = true
            };

            var nsfwSpy = new NsfwSpy();
            var result = nsfwSpy.ClassifyVideo(uri, videoOptions: videoOptions);

            Assert.True(result.IsNsfw);
            Assert.True(result.Frames.Count < 120); // This video has 120 frames
        }

        [Theory]
        [InlineData("bikini.avi")]
        [InlineData("bikini.mkv")]
        [InlineData("bikini.mp4")]
        [InlineData("bikini.webm")]
        public async Task ClassifyVideoFilePathAsync_ValidFilePath(string filename)
        {
            var filePath = Path.Combine(AppContext.BaseDirectory, $@"Assets\{filename}");

            var nsfwSpy = new NsfwSpy();
            var result = await nsfwSpy.ClassifyVideoAsync(filePath);

            if (filename.EndsWith(".webm"))
                Assert.Equal(180, result.Frames.Count);
            else
                Assert.Equal(181, result.Frames.Count);

            Assert.True(result.IsNsfw);
        }

        [Fact]
        public async Task ClassifyVideoUriAsync_ValidUri()
        {
            var uri = new Uri("https://i.imgur.com/MjTH5ZS.mp4");

            var nsfwSpy = new NsfwSpy();
            var result = await nsfwSpy.ClassifyVideoAsync(uri);

            Assert.Equal(120, result.Frames.Count);
            Assert.True(result.IsNsfw);
        }
    }
}
