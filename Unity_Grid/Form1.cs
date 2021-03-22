using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using UnityDrawing;

namespace Unity_Grid
{
    public partial class Form1 : Form
    {
        Panel panel = new Panel();

        public Form1()
        {
            InitializeComponent();

            this.Controls.Add(panel);
            Pen pen = new Pen(Color.Red, 2);
            AdjustableArrowCap myCap = new AdjustableArrowCap(5, 5);
            pen.CustomEndCap = myCap;

            Grid grid = new Grid();
            grid.Graphics.DrawLine(pen, 50, 200, 300, 50);

            grid.Fill(panel);
        }
    }
}
