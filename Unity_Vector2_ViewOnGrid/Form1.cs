using System.Windows.Forms;
using UnityDrawing;
using UnityEngine;
using Color = System.Drawing.Color;
using Grid = UnityDrawing.Grid;

namespace Unity_Vector2_ViewOnGrid
{
    public partial class Form1 : Form
    {
        Panel panel = new Panel();
        Grid grid;

        public Form1()
        {
            InitializeComponent();
            this.Controls.Add(panel);

            grid = new Grid();
            Vector2 v = new Vector2(300, 300);
            v.Write(Color.Red, 2, grid.Graphics);
            grid.Fill(panel);
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            Vector2 v = new Vector2(400, 200);
            v.Write(Color.Blue, 2, grid.Graphics);
            grid.Fill(panel);

            this.Controls.Remove(button1);
        }
    }
}
