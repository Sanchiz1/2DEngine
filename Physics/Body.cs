using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Physics
{
    public class Body
    {
        public double mass;
        public Vector2 position;
        public double velocity;
        public double rotaion;
        public Color color;
        public Body(double Mass, Vector2 Position, double Velocity, double Rotation, Color Color)
        {
            mass = Mass;
            position = Position;
            velocity = Velocity;
            rotaion = Rotation;
            color = Color;
        }

        public virtual void Move(Vector2 dir)
        {
            this.position += dir;
        }
        public void MoveTo(Vector2 dest)
        {
            this.position = dest;
        }
    }
}
