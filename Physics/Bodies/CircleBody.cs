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
        public override bool IsInside(Vector2 pos)
        {
            if ((pos - position).Length() <= diameter / 2) return true;
            return false;
        }

        public override void Move(Vector2 dir)
        {
            position += dir;
        }
        public override void MoveTo(Vector2 dest)
        {
            position = dest;
        }

        public override void Update(float deltaTime)
        {
            Vector2 acceleration = forces.Scale(1 / mass);
            linearVelocity += acceleration * deltaTime;
            position += linearVelocity * deltaTime;


            forces = new Vector2(0, 0);
        }
    }
}
