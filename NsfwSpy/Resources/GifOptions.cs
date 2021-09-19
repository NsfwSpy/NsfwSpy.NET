using System;
using System.Collections.Generic;
using System.Text;

namespace NsfwSpyNS
{
    /// <summary>
    /// Customise how the frames of a Gif file are classified.
    /// </summary>
    public class GifOptions
    {
        /// <summary>
        /// Stop classifying frames if a NSFW frame is found.
        /// </summary>
        public bool EarlyStopOnNsfw { get; set; } = false;

        /// <summary>
        /// Improve performance by only classifying one in every Nth frames e.g. 1 = classify all frames, 2 = classify 1 in 2 frames.
        /// </summary>
        public int ClassifyEveryNthFrame { get; set; } = 1;
    }
}
