using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Lab_6
{
    public class FileLidarReader
    {
        private readonly List<LidarFrame> frames = new();
        private int index;

        public int FrameCount => frames.Count;

        public FileLidarReader(string path)
        {
            foreach (var line in File.ReadLines(path))
            {
                if (string.IsNullOrWhiteSpace(line)) continue;
                if (!line.Contains(">")) continue;

                var parts = line.Split('>');
                if (parts.Length != 2) continue;

                if (!DateTime.TryParseExact(
                        parts[0],
                        "HH:mm:ss.ff",
                        CultureInfo.InvariantCulture,
                        DateTimeStyles.None,
                        out var time))
                    continue;

                var values = parts[1]
                    .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                    .Select(v => int.TryParse(v, out var d) ? d : 0)
                    .ToList();

                frames.Add(new LidarFrame
                {
                    Timestamp = time,
                    Distances = values
                });
            }
        }

        public LidarFrame GetNextFrame()
        {
            if (frames.Count == 0) return null;
            var frame = frames[index];
            index = (index + 1) % frames.Count;
            return frame;
        }
    }
}
