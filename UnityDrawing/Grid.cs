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
            int pbXOffset = 40;
            int pbYOffset = 15;
            int xLabelWidth = pbXOffset + 30;
            int yLabelWidth = pbXOffset + 10;
            int majorDelta = 50;
            int minorDelta = 10;

            Color majorAxisColor = Color.LightSteelBlue;
            Color minorAxisColor = Color.Gainsboro;
            Color axisTextColor = Color.SteelBlue;
            Pen majorAxisPen = new Pen(majorAxisColor, 1);
            Pen minorAxisPen = new Pen(minorAxisColor, 1);
            Pen majorAxisPenPartsSeparator = new Pen(majorAxisColor, 2);

            pb = new PictureBox();
            pb.Width = 1000;
            pb.Height = 1000;
            pb.Location = new Point(pbXOffset, pbYOffset);
            int xMin = 0;
            int yMin = 0;

            canvas = new Bitmap(pb.Width, pb.Height);
            Graphics = Graphics.FromImage(canvas);

            int i;
            int x, y;

            // Minor deltas.
            i = 0;
            do
            {
                x = minorDelta * i++;
                Graphics.DrawLine(minorAxisPen, x, 0, x, pb.Height);
            } 
            while (x < pb.Width);

            i = 0;
            do
            {
                y = minorDelta * i++;
                Graphics.DrawLine(minorAxisPen, 0, y, pb.Width, y);
            }
            while (y < pb.Height);

            // Major deltas.
            i = 0;
            
            do
            {
                x = majorDelta * i++;

                if ((xMin + x) % 250 == 0)
                {
                    Graphics.DrawLine(majorAxisPenPartsSeparator, x, 0, x, pb.Width);
                }
                else
                {
                    Graphics.DrawLine(majorAxisPen, x, 0, x, pb.Width);
                }

                Graphics.FillEllipse(new SolidBrush(axisTextColor), majorDelta * i - 3, -3, 6, 6);
                axisLabels.Add(new Label
                {
                    ForeColor = axisTextColor,
                    Location = new Point(x + pbXOffset / 2, 0),
                    TextAlign = ContentAlignment.MiddleCenter,
                    Text = (xMin + x).ToString(),
                    Width = xLabelWidth
                });
            } 
            while (x < pb.Width - majorDelta);

            i = 0;
            do
            {
                y = majorDelta * i++;

                if ((yMin + y) % 250 == 0)
                {
                    Graphics.DrawLine(majorAxisPenPartsSeparator, 0, y, pb.Width, y);
                }
                else
                {
                    Graphics.DrawLine(majorAxisPen, 0, y, pb.Width, y);
                }

                Graphics.FillEllipse(new SolidBrush(axisTextColor), -3, majorDelta * i - 3, 6, 6);
                axisLabels.Add(new Label
                {
                    ForeColor = axisTextColor,
                    Location = new Point(-11, majorDelta * i + pbYOffset / 2),
                    TextAlign = ContentAlignment.MiddleRight,
                    Text = (yMin + majorDelta * i).ToString(),
                    Width = yLabelWidth
                });
            }
            while (y < pb.Height - majorDelta);

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
