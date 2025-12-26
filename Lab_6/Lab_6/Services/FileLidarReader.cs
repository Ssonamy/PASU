using Lab_6.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace Lab_6.Services
{
    public class FileLidarReader
    {
        private readonly List<string> lines;
        private int currentIndex = 0;

        public bool EndOfFile => currentIndex >= lines.Count;

        public FileLidarReader(string filePath)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException("Файл данных лидара не найден");

            lines = new List<string>(File.ReadAllLines(filePath));
        }

        /// <summary>
        /// Считывает один фрейм (одну строку)
        /// </summary>
        public LidarFrame ReadNextFrame()
        {
            if (EndOfFile)
                return null;

            string line = lines[currentIndex++];
            return ParseFrame(line);
        }

        private LidarFrame ParseFrame(string line)
        {
            // Формат:
            // hh:mm:ss.nn> d1 d2 d3 ... dN

            int delimiterIndex = line.IndexOf('>');
            if (delimiterIndex < 0)
                throw new FormatException("Некорректный формат временной метки");

            string timePart = line.Substring(0, delimiterIndex);
            string dataPart = line.Substring(delimiterIndex + 1);

            DateTime timestamp = DateTime.ParseExact(
                timePart,
                "HH:mm:ss.ff",
                CultureInfo.InvariantCulture
            );

            string[] tokens = dataPart.Split(
                new[] { ' ' },
                StringSplitOptions.RemoveEmptyEntries
            );

            int[] distances = new int[tokens.Length];
            for (int i = 0; i < tokens.Length; i++)
                distances[i] = int.Parse(tokens[i]);

            return new LidarFrame(timestamp, distances);
        }
    }
}
