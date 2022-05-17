using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace NsfwSpyNS
{
    public interface INsfwSpy
    {
        NsfwSpyFramesResult ClassifyGif(byte[] gifImage, VideoOptions videoOptions = null);
        NsfwSpyFramesResult ClassifyGif(string filePath, VideoOptions videoOptions = null);
        NsfwSpyFramesResult ClassifyGif(Uri uri, WebClient webClient = null, VideoOptions videoOptions = null);
        Task<NsfwSpyFramesResult> ClassifyGifAsync(string filePath, VideoOptions videoOptions = null);
        Task<NsfwSpyFramesResult> ClassifyGifAsync(Uri uri, WebClient webClient = null, VideoOptions videoOptions = null);
        NsfwSpyResult ClassifyImage(byte[] imageData);
        NsfwSpyResult ClassifyImage(string filePath);
        NsfwSpyResult ClassifyImage(Uri uri, WebClient webClient = null);
        Task<NsfwSpyResult> ClassifyImageAsync(string filePath);
        Task<NsfwSpyResult> ClassifyImageAsync(Uri uri, WebClient webClient = null);
        List<NsfwSpyValue> ClassifyImages(IEnumerable<string> filesPaths, Action<string, NsfwSpyResult> actionAfterEachClassify = null);
        NsfwSpyFramesResult ClassifyVideo(byte[] video, VideoOptions videoOptions = null);
        NsfwSpyFramesResult ClassifyVideo(string filePath, VideoOptions videoOptions = null);
        NsfwSpyFramesResult ClassifyVideo(Uri uri, WebClient webClient = null, VideoOptions videoOptions = null);
        Task<NsfwSpyFramesResult> ClassifyVideoAsync(string filePath, VideoOptions videoOptions = null);
        Task<NsfwSpyFramesResult> ClassifyVideoAsync(Uri uri, WebClient webClient = null, VideoOptions videoOptions = null);
    }
}