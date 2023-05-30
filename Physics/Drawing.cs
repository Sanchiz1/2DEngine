using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Physics
{
    public static class Drawing
    {
        public static void DrawLine(PaintEventArgs e, Vector2 Start, Vector2 End, Pen pen, Vector2 zero)
        {
            e.Graphics.DrawLine(pen, 
                (int)(Start.X + zero.X), 
                (int)(Start.Y + zero.Y),
                (int)(End.X + zero.X),
                (int)(End.Y + zero.Y));
        }
        public static void DrawBox(PaintEventArgs e, BoxBody box, Vector2 zero)
        {
            Rectangle rectangle = new Rectangle(
                (int)(box.position.X - box.width / 2 + zero.X),
                (int)(box.position.Y - box.height / 2 + zero.Y),
                (int)(box.width),
                (int)(box.height));
            e.Graphics.TranslateTransform((float)(box.position.X + zero.X), (float)(box.position.Y + zero.Y));
            e.Graphics.RotateTransform((float)box.rotaion);
            e.Graphics.TranslateTransform((float)-(box.position.X + zero.X), (float)-(box.position.Y + zero.Y));
            e.Graphics.DrawRectangle(new Pen(box.color, 1), rectangle);
            e.Graphics.FillRectangle(new SolidBrush(box.color), rectangle);
            e.Graphics.ResetTransform();

        }
        public static void DrawCircle(PaintEventArgs e, CircleBody circle, Vector2 zero)
        {
            RectangleF Circle = new RectangleF((float)(circle.position.X + zero.X - circle.diameter / 2), (float)(circle.position.Y + zero.Y - circle.diameter / 2), (float)circle.diameter, (float)circle.diameter);
            e.Graphics.DrawEllipse(new Pen(circle.color, 1), Circle);
            e.Graphics.FillEllipse(new SolidBrush(circle.color), Circle);
        }
    }
}
