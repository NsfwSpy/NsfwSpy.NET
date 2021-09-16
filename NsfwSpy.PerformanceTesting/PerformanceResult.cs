using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NsfwSpy.PerformanceTesting
{
    class PerformanceResult
    {
        public string Key { get; set; }
        public int CorrectAsserts { get; set; }
        public int CorrectSpecialAsserts { get; set; }
        public int FailedAsserts { get; set; }
        public int FailedSpecialAsserts { get; set; }
        public int TotalAsserts => CorrectAsserts + FailedAsserts;
        public int TotalSpecialAsserts => CorrectSpecialAsserts + FailedSpecialAsserts;

        public PerformanceResult(string key)
        {
            Key = key;
        }
    }
}
