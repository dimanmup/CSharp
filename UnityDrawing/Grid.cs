using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace UnityDrawing
{
    public class Grid
    {
        private Bitmap canvas;
        private PictureBox pb;
        private List<Label> axisLabels = new List<Label>();
        public Graphics Graphics;

        public Grid()
        {
            int pbXOffset = 30;
            int pbYOffset = 15;
            int majorDelta = 50;
            int minorDelta = 10;
            Color majorAxisColor = Color.LightSteelBlue;
            Color minorAxisColor = Color.Gainsboro;
            Color axisTextColor = Color.SteelBlue;
            Pen majorAxisPen = new Pen(majorAxisColor, 1);
            Pen minorAxisPen = new Pen(minorAxisColor, 1);
            int i;

            pb = new PictureBox();
            pb.Width = 3_000;
            pb.Height = 3_000;
            pb.Location = new Point(pbXOffset, pbYOffset);

            canvas = new Bitmap(pb.Width, pb.Height);
            Graphics = Graphics.FromImage(canvas);

            // Minor deltas.
            i = 0;
            while (minorDelta * i < pb.Width)
            {
                Graphics.DrawLine(minorAxisPen, minorDelta * i, 0, minorDelta * i, pb.Height);
                i++;
            }
            i = 0;
            while (minorDelta * i < pb.Width)
            {
                Graphics.DrawLine(minorAxisPen, 0, minorDelta * i, pb.Width, minorDelta * i);
                i++;
            }

            // Major deltas.
            i = 0;
            while (majorDelta * i++ < pb.Width)
            {
                Graphics.DrawLine(majorAxisPen, majorDelta * i, 0, majorDelta * i, pb.Width);
                Graphics.FillEllipse(new SolidBrush(axisTextColor), majorDelta * i - 3, -3, 6, 6);
                axisLabels.Add(new Label
                {
                    ForeColor = axisTextColor,
                    Location = new Point(majorDelta * i + pbXOffset / 2, 0),
                    TextAlign = ContentAlignment.MiddleCenter,
                    Text = (majorDelta * i).ToString(),
                    Width = pbXOffset
                });
            }
            i = 0;
            while (majorDelta * i++ < pb.Height)
            {
                Graphics.DrawLine(majorAxisPen, 0, majorDelta * i, pb.Width, majorDelta * i);
                Graphics.FillEllipse(new SolidBrush(axisTextColor), -3, majorDelta * i - 3, 6, 6);
                axisLabels.Add(new Label
                {
                    ForeColor = axisTextColor,
                    Location = new Point(0, majorDelta * i + pbYOffset / 2),
                    TextAlign = ContentAlignment.MiddleRight,
                    Text = (majorDelta * i).ToString(),
                    Width = pbXOffset
                });
            }
        }

        public void Fill(Panel p)
        {
            pb.Image = canvas;

            if (p.Controls.Contains(pb))
            {
                p.Controls.Remove(pb);
            }

            p.Dock = DockStyle.Fill;
            p.AutoScroll = true;
            p.Controls.Add(pb);

            pb.BringToFront();

            // Adding labels.
            foreach (Label lb in axisLabels)
            {
                p.Controls.Add(lb);
                lb.Height = 15;
                lb.Font = new Font("Consolas", 9);
                lb.BringToFront();
            }
        }
    }
}
