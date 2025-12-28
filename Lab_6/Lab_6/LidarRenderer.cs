using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Lab_6
{
    public class LidarRender : Panel
    {
        private List<int> distances = new();
        private readonly object sync = new();

        public float Scale = 0.05f;

        public LidarRender()
        {
            DoubleBuffered = true;
            BackColor = Color.White;
        }

        public void UpdateFrame(List<int> data)
        {
            lock (sync)
                distances = new List<int>(data);

            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            e.Graphics.TranslateTransform(Width / 2, Height / 2);

            lock (sync)
            {
                if (distances.Count == 0) return;

                float step = 360f / distances.Count;

                for (int i = 0; i < distances.Count; i++)
                {
                    float angle = i * step * (float)Math.PI / 180f;
                    float r = distances[i] * Scale;

                    float x = r * (float)Math.Cos(angle);
                    float y = r * (float)Math.Sin(angle);

                    e.Graphics.FillEllipse(
                        Brushes.OrangeRed,
                        x - 2, y - 2, 4, 4
                    );
                }
            }
        }
    }
}
