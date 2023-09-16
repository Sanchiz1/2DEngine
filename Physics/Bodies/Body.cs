using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Physics
{
    public abstract class Body
    {
        public double mass;
        public Vector2 position;
        public double velocity;
        public Vector2 linearVelocity;
        public Vector2 forces;
        public double rotaion;
        public Color color;
        public Body(double Mass, Vector2 Position, double Velocity, double Rotation, Color Color)
        {
            mass = Mass;
            position = Position;
            velocity = Velocity;
            rotaion = Rotation;
            color = Color;
            forces = new Vector2(0, 0);
            linearVelocity = new Vector2(0, 0);
        }

        public abstract bool IsInside(Vector2 pos);
        public abstract void Move(Vector2 dir);
        public abstract void MoveTo(Vector2 dest);

        public void ApplyForce(Vector2 Force)
        {
            forces += Force;
        }

        public abstract void Update(float deltaTime);
    }
}
