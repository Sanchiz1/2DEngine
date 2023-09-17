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
        public PolygonBody(double Mass, double Restitution, Vector2 Position, double Velocity, double Rotation, Color Color, Vector2[] Vertcies) : base(Mass, Position, Velocity, Rotation, Color, Restitution)
        {
            vertcies = Vertcies;
            position = vertcies[0];
        }
        public PolygonBody(double Mass, double Restitution, Vector2 Position, double Velocity, double Rotation, Color Color, double width, double height) : base(Mass, Position, Velocity, Rotation, Color, Restitution)
        {
            position = Position;
            vertcies = new Vector2[] { position, new Vector2(position.X + width, position.Y), new Vector2(position.X + width, position.Y + height), new Vector2(position.X, position.Y + height) };
        }
        public Vector2[] GetTransformedVertices()
        {
            Vector2 center = Collisions.FindArithmeticMean(this.vertcies);
            Vector2[] v = new Vector2[vertcies.Length];
            for (int i = 0; i < vertcies.Length; ++i)
            {
                v[i] = new Vector2((float)(vertcies[i].X - center.X) * Math.Cos(PhysicsMath.DegreesToRadians(rotaion)) - (vertcies[i].Y - center.Y) * Math.Sin(PhysicsMath.DegreesToRadians(rotaion)) + center.X,
                    (float)(vertcies[i].X - center.X) * Math.Sin(PhysicsMath.DegreesToRadians(rotaion)) + (vertcies[i].Y - center.Y) * Math.Cos(PhysicsMath.DegreesToRadians(rotaion)) + center.Y
                    );
            }
            return v;
        }

        public override bool IsInside(Vector2 pos)
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
            position += dir;
        }
        public override void MoveTo(Vector2 dir)
        {
            Vector2 addition = Collisions.FindArithmeticMean(vertcies) - dir;
            for (int i = 0; i < vertcies.Length; ++i)
            {
                vertcies[i] -= addition;
            }
            position -= addition;
        }

        public override void Update(float deltaTime)
        {
            Vector2 acceleration = forces.Scale(1 / mass);
            linearVelocity += acceleration * deltaTime;
            this.Move(linearVelocity * deltaTime);


            forces = new Vector2(0, 0);
        }
    }
}
