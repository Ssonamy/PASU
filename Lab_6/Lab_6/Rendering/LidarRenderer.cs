using Lab_6.Models;
using System;
using System.Drawing;

namespace Lab_6.Rendering
{
    public class LidarRenderer
    {
        private readonly int imageSize;
        private readonly float scale; // мм → пиксели

        public LidarRenderer(int imageSize, float scaleMmToPixel)
        {
            this.imageSize = imageSize;
            this.scale = scaleMmToPixel;
        }

        public Bitmap Render(LidarFrame frame)
        {
            Bitmap bmp = new Bitmap(imageSize, imageSize);

            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.Clear(Color.Black);

                int centerX = imageSize / 2;
                int centerY = imageSize / 2;

                // Робот в центре
                g.FillEllipse(
                    Brushes.Red,
                    centerX - 4,
                    centerY - 4,
                    8,
                    8
                );

                int count = frame.Distances.Length;
                double angleStep = 2.0 * Math.PI / count;

                for (int i = 0; i < count; i++)
                {
                    int distance = frame.Distances[i];
                    if (distance <= 0)
                        continue;

                    double angle = i * angleStep;

                    float x = centerX + (float)(Math.Cos(angle) * distance / scale);
                    float y = centerY + (float)(Math.Sin(angle) * distance / scale);

                    // Отрисовка точки препятствия
                    g.FillRectangle(
                        Brushes.Lime,
                        x - 2,
                        y - 2,
                        4,
                        4
                    );
                }
            }

            return bmp;
        }
    }
}
