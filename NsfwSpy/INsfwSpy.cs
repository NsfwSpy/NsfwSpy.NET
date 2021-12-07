using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace NsfwSpyNS
{
    public interface INsfwSpy
    {
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
        NsfwSpyFramesResult ClassifyMp4(byte[] video, VideoOptions videoOptions = null);
        NsfwSpyFramesResult ClassifyMp4(string filePath, VideoOptions videoOptions = null);
        NsfwSpyFramesResult ClassifyMp4(Uri uri, WebClient webClient = null, VideoOptions videoOptions = null);
        Task<NsfwSpyFramesResult> ClassifyMp4Async(string filePath, VideoOptions videoOptions = null);
        Task<NsfwSpyFramesResult> ClassifyMp4Async(Uri uri, WebClient webClient = null, VideoOptions videoOptions = null);
    }
}