using System;
using System.Collections.Generic;

namespace Lab_6
{
    public class LidarFrame
    {
        public DateTime Timestamp { get; set; }
        public List<int> Distances { get; set; } = new List<int>();
    }
}
