using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NsfwSpyNS.PerformanceTesting
{
    class ClassificationType
    {
        internal string Key { get; set; }
        internal bool IsNsfw { get; set; }

        public ClassificationType(string key, bool isNsfw)
        {
            Key = key;
            IsNsfw = isNsfw;
        }
    }
}
