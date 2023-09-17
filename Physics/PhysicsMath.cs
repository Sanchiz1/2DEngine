using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Physics
{
    public static class PhysicsMath
    {
        public static int Min(int a, int b)
        {
            return a < b ? a : b;
        }
        public static double Min(double a, double b)
        {
            return a < b ? a : b;
        }

        public static double DegreesToRadians(double degrees)
        {
            return degrees * Math.PI / 180.0;
        }

        public static PointF[] GetPointsFromVector2(Vector2[] v, Vector2 zero, int vertciesNumber)
        {
            PointF[] p = new PointF[vertciesNumber];
            for (int i = 0; i < v.Length; ++i)
            {
                p[i] = new PointF((float)(v[i].X + zero.X), (float)(v[i].Y + zero.Y));
            }
            return p;
        }
    }
}
