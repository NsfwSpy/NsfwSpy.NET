using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NsfwSpy
{
    public class NsfwSpyValue
    {
        public string FilePath { get; init; }
        public NsfwSpyResult Result { get; init; }

        public NsfwSpyValue(string filePath, NsfwSpyResult result)
        {
            FilePath = filePath;
            Result = result;
        }
    }
}
