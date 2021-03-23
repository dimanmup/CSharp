using System.Drawing;
using System.Drawing.Drawing2D;
using UnityEngine;
using Color = System.Drawing.Color;
using Graphics = System.Drawing.Graphics;

namespace UnityDrawing
{
    public static class XVector
    {
        public static void Write(this Vector2 vector, Color color, int width, Graphics gr)
        {
            Pen pen = new Pen(color, width);
            pen.CustomEndCap = new AdjustableArrowCap(5, 5);
            gr.DrawLine(pen, 0, 0, vector.x, vector.y);
        }
    }
}
