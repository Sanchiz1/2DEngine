using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Physics
{
    public class PolygonBody : Body
    {
        public Vector2[] vertcies;
        public PolygonBody(double Mass, Vector2 Position, double Velocity, double Rotation, Color Color, Vector2[] Vertcies) : base(Mass, Position, Velocity, Rotation, Color)
        {
            vertcies = Vertcies;
            position = vertcies[0];
        }
        public Vector2[] GetTransformedVerticies()
        {
            Vector2 center = Collisions.FindArithmeticMean(this.vertcies);
            Vector2[] v = new Vector2[vertcies.Length];
            for (int i = 0; i < vertcies.Length; ++i)
            {
                v[i] = new Vector2((float)(vertcies[i].X - center.X) * Math.Cos(DegreesToRadians(rotaion)) - (vertcies[i].Y - center.Y) * Math.Sin(DegreesToRadians(rotaion)) + center.X,
                    (float)(vertcies[i].X - center.X) * Math.Sin(DegreesToRadians(rotaion)) + (vertcies[i].Y - center.Y) * Math.Cos(DegreesToRadians(rotaion)) + center.Y
                    );
            }
            return v;
        }
        double DegreesToRadians(double degrees)
        {
            return degrees * Math.PI / 180.0;
        }

        public PointF[] GetPoints(Vector2[] v, Vector2 zero)
        {
            PointF[] p = new PointF[vertcies.Length];
            for (int i = 0; i < v.Length; ++i)
            {
                p[i] = new PointF((float)(v[i].X + zero.X), (float)(v[i].Y + zero.Y));
            }
            return p;
        }
        public bool IsInside(Vector2 pos)
        {
            return Physics.Collisions.IsDotInside(vertcies, pos);
        }

        public string VertciesToString()
        {
            string s = "";
            for (int i = 0; i < vertcies.Length; ++i)
            {
                s += vertcies[i].ToString();
            }
            return s;
        }
        public override void Move(Vector2 dir)
        {
            for (int i = 0; i < vertcies.Length; ++i)
            {
                vertcies[i] += dir;
            }
        }
        public override void MoveTo(Vector2 dir)
        {
            Vector2 addition = Collisions.FindArithmeticMean(vertcies) - dir;
            for (int i = 0; i < vertcies.Length; ++i)
            {
                vertcies[i] -= addition;
            }
        }
    }
}
