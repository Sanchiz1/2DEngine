using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Physics
{
    public class CircleBody : Body
    {
        public double diameter;
        public CircleBody(double Mass, Vector2 Position, double Velocity, double Rotation, Color Color, double Diameter) : base(Mass, Position, Velocity, Rotation, Color)
        {
            diameter = Diameter;
        }
        public  bool IsInside(Vector2 pos)
        {
            if ((pos - position).Length() <= diameter / 2) return true;
            return false;
        }
    }
}
