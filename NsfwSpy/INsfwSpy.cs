using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace NsfwSpyNS
{
    public interface INsfwSpy
    {
        NsfwSpyGifResult ClassifyGif(string filePath, GifOptions gifOptions = null);
        NsfwSpyGifResult ClassifyGif(Uri uri, WebClient webClient = null, GifOptions gifOptions = null);
        Task<NsfwSpyGifResult> ClassifyGifAsync(string filePath, GifOptions gifOptions = null);
        Task<NsfwSpyGifResult> ClassifyGifAsync(Uri uri, WebClient webClient = null, GifOptions gifOptions = null);
        NsfwSpyResult ClassifyImage(byte[] imageData);
        NsfwSpyResult ClassifyImage(string filePath);
        NsfwSpyResult ClassifyImage(Uri uri, WebClient webClient = null);
        Task<NsfwSpyResult> ClassifyImageAsync(string filePath);
        Task<NsfwSpyResult> ClassifyImageAsync(Uri uri, WebClient webClient = null);
        List<NsfwSpyValue> ClassifyImages(IEnumerable<string> filesPaths, Action<string, NsfwSpyResult> actionAfterEachClassify = null);
    }
}