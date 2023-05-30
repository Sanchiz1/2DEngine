using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Physics
{
    public class BoxBody : Body
    {
        public double width;
        public double height;

        public BoxBody(double Mass, Vector2 Position, double Velocity, double Rotation, Color Color, double Width, double Height) : base (Mass, Position, Velocity, Rotation, Color) {
            width = Width;
            height = Height;
        }
        public bool IsInside(Vector2 pos)
        {
            if(pos.X > position.X - width / 2 && pos.X < position.X + width / 2 && pos.Y > position.Y - height / 2 && pos.Y < position.Y + height / 2) return true;
            return false;
        }
        public Vector2[] GetVerticies()
        {
            Vector2[] verticies = { 
                new Vector2(position.X - width / 2, position.Y - height / 2),
                new Vector2(position.X + width / 2, position.Y - height / 2),
                new Vector2(position.X + width / 2, position.Y + height / 2),
                new Vector2(position.X - width / 2, position.Y + height / 2),
            };
            return verticies;
        }
        public Vector2[] GetTransformedVerticies()
        {
            Vector2[] verticies = {
                new Vector2((-width / 2) * Math.Cos(DegreesToRadians(rotaion)) - (-height / 2) * Math.Sin(DegreesToRadians(rotaion)) + position.X, (-width / 2) * Math.Sin(DegreesToRadians(rotaion)) - (height / 2) * Math.Cos(DegreesToRadians(rotaion)) + position.Y),
                new Vector2((width / 2) * Math.Cos(DegreesToRadians(rotaion)) - (-height / 2) * Math.Sin(DegreesToRadians(rotaion)) + position.X, (width / 2) * Math.Sin(DegreesToRadians(rotaion)) + (-height / 2) * Math.Cos(DegreesToRadians(rotaion)) + position.Y),
                new Vector2((width / 2) * Math.Cos(DegreesToRadians(rotaion)) - (height / 2) * Math.Sin(DegreesToRadians(rotaion)) + position.X, (width / 2) * Math.Sin(DegreesToRadians(rotaion)) - (-height / 2) * Math.Cos(DegreesToRadians(rotaion)) + position.Y),
                new Vector2((-width / 2) * Math.Cos(DegreesToRadians(rotaion)) - (height / 2) * Math.Sin(DegreesToRadians(rotaion)) + position.X, (-width / 2) * Math.Sin(DegreesToRadians(rotaion)) - (-height / 2) * Math.Cos(DegreesToRadians(rotaion)) + position.Y),
            };
            return verticies;
        }
        double DegreesToRadians(double degrees)
        {
            return degrees * Math.PI / 180.0;
        }
    }
}
