using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.AxHost;

namespace Physics
{
    public static class Drawing
    {
        public static void DrawLine(PaintEventArgs e, Vector2 Start, Vector2 End, Pen pen, Vector2 zero, float scale)
        {
            e.Graphics.ScaleTransform(scale, scale);
            e.Graphics.DrawLine(pen, 
                (int)(Start.X + zero.X), 
                (int)(Start.Y + zero.Y),
                (int)(End.X + zero.X),
                (int)(End.Y + zero.Y));
        }
        public static void DrawBox(PaintEventArgs e, BoxBody box, Vector2 zero, float scale)
        {
            e.Graphics.ScaleTransform(scale, scale);
            Rectangle rectangle = new Rectangle(
                (int)(box.position.X - box.width / 2 + zero.X),
                (int)(box.position.Y - box.height / 2 + zero.Y),
                (int)(box.width),
                (int)(box.height));
            
            e.Graphics.TranslateTransform((float)(box.position.X + zero.X), (float)(box.position.Y + zero.Y));
            e.Graphics.RotateTransform((float)box.rotaion);
            e.Graphics.TranslateTransform((float)-(box.position.X + zero.X), (float)-(box.position.Y + zero.Y));
            e.Graphics.FillRectangle(new SolidBrush(box.color), rectangle);
            e.Graphics.DrawRectangle(new Pen(Color.Silver, 2), rectangle);
            e.Graphics.ResetTransform();
        }
        public static void DrawCircle(PaintEventArgs e, CircleBody circle, Vector2 zero, float scale)
        {
            e.Graphics.ScaleTransform(scale, scale);
            RectangleF Circle = new RectangleF((float)(circle.position.X + zero.X - circle.diameter / 2), (float)(circle.position.Y + zero.Y - circle.diameter / 2), (float)circle.diameter, (float)circle.diameter);
            e.Graphics.FillEllipse(new SolidBrush(circle.color), Circle);
            e.Graphics.DrawEllipse(new Pen(Color.Silver, 2), Circle);
            e.Graphics.ResetTransform();
        }

        public static void DrawPolygon(PaintEventArgs e, PolygonBody polygon, Vector2 zero, float scale)
        {
            e.Graphics.ScaleTransform(scale, scale);
            e.Graphics.FillPolygon(new SolidBrush(polygon.color), polygon.GetPoints(polygon.GetTransformedVerticies(), zero));
            e.Graphics.DrawPolygon(new Pen(Color.Silver, 2), polygon.GetPoints(polygon.GetTransformedVerticies(), zero));
            e.Graphics.ResetTransform();
        }
    }
}
