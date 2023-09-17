using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Physics.Bodies
{
    public static class BodyFactory
    {
        public static Body CreateBoxBody(double Mass, double Restitution, Vector2 Position, double Velocity, double Rotation, Color Color, double Width, double Height)
        {
            return new PolygonBody(Mass, Restitution, Position, Velocity, Rotation, Color, Width, Height);
        }
        public static Body CreateCircleBody(double Mass, double Restitution, Vector2 Position, double Velocity, double Rotation, Color Color, double Diameter)
        {
            return new CircleBody(Mass, Restitution, Position, Velocity, Rotation, Color, Diameter);
        }
        public static Body CreatePolygonBody(double Mass, double Restitution, Vector2 Position, double Velocity, double Rotation, Color Color, Vector2[] Vertcies)
        {
            return new PolygonBody( Mass, Restitution, Position,  Velocity,  Rotation,  Color, Vertcies);
        }
    }
}
