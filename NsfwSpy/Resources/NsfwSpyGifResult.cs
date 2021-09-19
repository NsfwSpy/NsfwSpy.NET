using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NsfwSpyNS
{
    /// <summary>
    /// The result from classifying a Gif file.
    /// </summary>
    public class NsfwSpyGifResult
    {
        /// <summary>
        /// The NsfwSpyResults for each of the frames classified with the key being the frame index.
        /// </summary>
        public Dictionary<int, NsfwSpyResult> Frames { get; set; }

        /// <summary>
        /// The amount of frames classified.
        /// </summary>
        public int FrameCount => Frames.Count;

        /// <summary>
        /// True if any of the frames have been classified as NSFW.
        /// </summary>
        public bool IsNsfw => Frames.Any(f => f.Value.IsNsfw);

        public NsfwSpyGifResult(Dictionary<int, NsfwSpyResult> frames)
        {
            Frames = frames;
        }
    }
}
