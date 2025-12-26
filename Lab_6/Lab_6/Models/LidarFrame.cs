using System;

namespace Lab_6.Models
{
    public class LidarFrame
    {
        public DateTime Timestamp { get; set; }
        public int[] Distances { get; set; }

        public LidarFrame(DateTime timestamp, int[] distances)
        {
            Timestamp = timestamp;
            Distances = distances;
        }
    }
}
