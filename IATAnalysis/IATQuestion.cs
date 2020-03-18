using System;
using System.Collections.Generic;
using System.Text;

namespace IATAnalysis
{
    class IATQuestion
    {
        public string LeftTitle1 { get; set; }
        public string LeftTitle2 { get; set; }
        public string RightTitle1 { get; set; }
        public string RightTitle2 { get; set; }
        public string DetectValue { get; set; }
        public bool IsValueImage { get; set; }
        public bool IsValueText { get; set; }
        public bool IsRiteValueRight { get; set; }
        public bool IsDetectionWrong { get; set; }
        public int TimeStamp { get; set; } // in ms
        public int CompletedTimeStamp { get; set; }
        public bool Completed { get; set; }
    }
}
