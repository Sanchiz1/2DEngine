using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Physics
{
    public class Vector2
    {
        private double x, y;

        public double Y
        {
            get { return y; }
            set { y = value; }
        }

        public double X
        {
            get { return x; }
            set { x = value; }
        }
        public Vector2()
        {
            x = 0.0;
            y = 0.0;
        }
        public Vector2(double x1, double y1)
        {
            x = x1;
            y = y1;
        }
        public Vector2(Vector2 v)
        {
            x = v.x;
            y = v.y;
        }
        public override string ToString()
        {
            return "(" + x.ToString("g3") + "," + y.ToString("g3") + ")";
        }

        public double Length()
        {
            return Math.Sqrt(x * x + y * y);
        }

        public bool Equal(Vector2 v2)
        {
            if (x == v2.X && y == v2.Y)
                return true;
            else
                return false;
        }
        public double DotProduct(Vector2 v2)
        {
            return x * v2.X + y * v2.Y;
        }
        public void Normalize(Vector2 v)
        {
            double len = v.Length();
            X = v.X / len;
            Y = v.y / len;
        }
        public double AngleBetween(Vector2 v2)
        {
            double answer = 0;
            double top = DotProduct(v2);
            double under = Length() * v2.Length();
            double angle;
            if (under != 0)
                answer = top / under;
            else
                return 0;
            if (answer > 1) answer = 1;
            if (answer < -1) answer = -1;
            angle = Math.Acos(answer);
            return angle * 180 / Math.PI;
        }
        public Vector2 Unit()
        {
            double length = Math.Sqrt(x * x + y * y);
            return new Vector2(x / length, y / length);
        }

        public Vector2 ParralelComponent(Vector2 v2)
        {
            double lengthSquared, dotProduct, scale;
            lengthSquared = Length() * Length();
            dotProduct = DotProduct(v2);
            if (lengthSquared != 0)
                scale = dotProduct / lengthSquared;
            else
                return new Vector2();
            return new Vector2(Scale(scale));
        }

        public Vector2 PerpendicularComponent(Vector2 v2)
        {
            return new Vector2(v2 - ParralelComponent(v2));
        }

        public Vector2 Scale(double scale)
        {
            return new Vector2(scale * x, scale * y);
        }

        public static Vector2 operator *(double k, Vector2 v1)
        {
            return new Vector2(k * v1.x, k * v1.y);
        }

        public static Vector2 operator *(Vector2 v1, double k)
        {
            return new Vector2(k * v1.x, k * v1.y);
        }

        public static Vector2 operator +(Vector2 v1, Vector2 v2)
        {
            return new Vector2(v1.X + v2.X, v1.Y + v2.Y);
        }

        public static Vector2 operator -(Vector2 v1, Vector2 v2)
        {
            return new Vector2(v1.X - v2.X, v1.Y - v2.Y);
        }
        public static Vector2 operator -(Vector2 v1)
        {
            return new Vector2(-v1.x, -v1.y);
        }
    }
}
