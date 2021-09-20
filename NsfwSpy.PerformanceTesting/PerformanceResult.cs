using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NsfwSpyNS.PerformanceTesting
{
    class PerformanceResult
    {
        public string Key { get; set; }
        public List<NsfwSpyResult> Results { get; set; }
        public int CorrectAsserts => Results.Count(r => r.PredictedLabel == Key);
        public int NsfwAsserts => Results.Count(r => r.IsNsfw);
        public int DrawingAsserts => Results.Count(r => r.PredictedLabel == "Drawing");
        public int HentaiAsserts => Results.Count(r => r.PredictedLabel == "Hentai");
        public int NeutralAsserts => Results.Count(r => r.PredictedLabel == "Neutral");
        public int PornographyAsserts => Results.Count(r => r.PredictedLabel == "Pornography");
        public int SexyAsserts => Results.Count(r => r.PredictedLabel == "Sexy");
        public int TotalAsserts => Results.Count();

        public PerformanceResult(string key)
        {
            Key = key;
            Results = new List<NsfwSpyResult>();
        }
    }
}
