using System;
using System.Collections.Generic;
using System.Text;

namespace NsfwSpyNS
{
    public class ClassificationFailedException : Exception
    {
        public ClassificationFailedException(string message)
            : base(message)
        {
        }
    }
}
