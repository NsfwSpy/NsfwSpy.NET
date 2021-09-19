using System;
using System.Collections.Generic;
using System.Text;

namespace NsfwSpyNS
{
    public class GifOptions
    {
        public bool EarlyStopOnNsfw { get; set; } = false;
        public int ClassifyEveryNthFrame { get; set; } = 1;
    }
}
