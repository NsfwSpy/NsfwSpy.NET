using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NsfwSpyNS
{
    public class NsfwSpyGifResult
    {
        public Dictionary<int, NsfwSpyResult> Frames { get; set; }
        public int FrameCount => Frames.Count;
        public bool IsNsfw => Frames.Any(f => f.Value.IsNsfw);

        public NsfwSpyGifResult(Dictionary<int, NsfwSpyResult> frames)
        {
            Frames = frames;
        }
    }
}
