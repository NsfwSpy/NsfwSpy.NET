using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NsfwSpyNS
{
    public class NsfwSpyValue
    {
        public string FilePath { get; }
        public NsfwSpyResult Result { get; }

        public NsfwSpyValue(string filePath, NsfwSpyResult result)
        {
            FilePath = filePath;
            Result = result;
        }
    }
}
