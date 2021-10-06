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
            var uri = new Uri("https://www.gardeningknowhow.com/wp-content/uploads/2021/07/sulfur-cosmos-mexican-aster-flowers.jpg");

            var nsfwSpy = new NsfwSpy();
            var result = nsfwSpy.ClassifyImage(uri);

            Assert.Equal("Neutral", result.PredictedLabel);
        }

        [Fact]
        public void ClassifyImageUri_CustomWebClient()
        {
            var uri = new Uri("https://www.gardeningknowhow.com/wp-content/uploads/2021/07/sulfur-cosmos-mexican-aster-flowers.jpg");
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
            var uri = new Uri("https://www.gardeningknowhow.com/wp-content/uploads/2021/07/sulfur-cosmos-mexican-aster-flowers.jpg");

            var nsfwSpy = new NsfwSpy();
            var result = await nsfwSpy.ClassifyImageAsync(uri);

            Assert.Equal("Neutral", result.PredictedLabel);
        }

        [Fact]
        public async Task ClassifyImageUriAsync_CustomWebClient()
        {
            var uri = new Uri("https://www.gardeningknowhow.com/wp-content/uploads/2021/07/sulfur-cosmos-mexican-aster-flowers.jpg");
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
            var gifOptions = new GifOptions
            {
                ClassifyEveryNthFrame = 2
            };

            var nsfwSpy = new NsfwSpy();
            var result = nsfwSpy.ClassifyGif(filePath, gifOptions);

            Assert.Equal(5, result.Frames.Count);
            Assert.False(result.IsNsfw);
        }

        [Fact]
        public void ClassifyGifFilePath_EndEarlyOnNsfw()
        {
            var filePath = Path.Combine(AppContext.BaseDirectory, @"Assets/bikini.gif");
            var gifOptions = new GifOptions
            {
                EarlyStopOnNsfw = true
            };

            var nsfwSpy = new NsfwSpy();
            var result = nsfwSpy.ClassifyGif(filePath, gifOptions: gifOptions);

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
            var gifOptions = new GifOptions
            {
                ClassifyEveryNthFrame = 2
            };

            var nsfwSpy = new NsfwSpy();
            var result = nsfwSpy.ClassifyGif(uri, gifOptions: gifOptions);

            Assert.Equal(5, result.Frames.Count);
            Assert.False(result.IsNsfw);
        }

        [Fact]
        public void ClassifyGifUri_EndEarlyOnNsfw()
        {
            var uri = new Uri("https://c.tenor.com/5y-jOowm51MAAAAd/bikini.gif");
            var gifOptions = new GifOptions
            {
                EarlyStopOnNsfw = true
            };

            var nsfwSpy = new NsfwSpy();
            var result = nsfwSpy.ClassifyGif(uri, gifOptions: gifOptions);

            Assert.True(result.IsNsfw);
            Assert.True(result.Frames.Count < 181); // This Gif has 181 frames
        }
    }
}
