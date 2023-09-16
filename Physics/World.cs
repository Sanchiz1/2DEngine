using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.AxHost;

namespace Physics
{
    public class World
    {
        public List<Body> bodies = new List<Body>();
        public void AddBody(Body body)
        {
            bodies.Add(body);
        }
        public void RemoveBody(Body body)
        {
            bodies.Remove(body);
        }

        public void UpdateWorld()
        {
            for (int i = 0; i < bodies.Count; ++i)
            {
                bodies[i].Update((float)0.1);
                for (int j = i + 1; j < bodies.Count; ++j)
                {
                    if (bodies[i].GetType() == typeof(CircleBody))
                    {
                        if (bodies[j].GetType() == typeof(CircleBody))
                        {
                            if (Collisions.CirclesCollision((CircleBody)bodies[i], (CircleBody)bodies[j]))
                            {
                                Collisions.ResolveCirclesCollision((CircleBody)bodies[i], (CircleBody)bodies[j]);
                            };
                        }
                        else
                        {
                            if (Collisions.IntersectCirclePolygon(
                                ((CircleBody)bodies[i]).position,
                                (float)((CircleBody)bodies[i]).diameter / 2,
                                ((PolygonBody)bodies[j]).GetTransformedVertices(),
                                out Vector2 normal,
                                out float depth))
                            {
                                Collisions.ResolvePolygonCircleCollision((CircleBody)bodies[i], (PolygonBody)bodies[j], normal, depth);
                            }
                        }
                    }
                    else
                    {
                        if (bodies[j].GetType() == typeof(CircleBody))
                        {
                            if (Collisions.IntersectCirclePolygon(
                                ((CircleBody)bodies[j]).position,
                                (float)((CircleBody)bodies[j]).diameter / 2,
                                ((PolygonBody)bodies[i]).GetTransformedVertices(),
                                out Vector2 normal,
                                out float depth))
                            {
                                Collisions.ResolvePolygonCircleCollision((CircleBody)bodies[j], (PolygonBody)bodies[i], normal, depth);
                            }
                        }
                        else
                        {
                            if (Collisions.IntersectPolygons(
                                ((PolygonBody)bodies[i]).GetTransformedVertices(),
                                ((PolygonBody)bodies[j]).GetTransformedVertices(),
                                out Vector2 normal,
                                out float depth))
                            {
                                Collisions.ResolvePolygonsCollision((PolygonBody)bodies[i], (PolygonBody)bodies[j], normal, depth);
                            }
                        }
                    }
                }
            }
        }
        public void DrawWorld(PaintEventArgs e, Vector2 zero, float scale)
        {
            for (int i = 0; i < bodies.Count; ++i)
            {
                if (bodies[i].GetType() == typeof(CircleBody))
                {
                    Drawing.DrawCircle(e, (CircleBody)bodies[i], zero, scale);
                }
                if (bodies[i].GetType() == typeof(PolygonBody))
                {
                    Drawing.DrawPolygon(e, (PolygonBody)bodies[i], zero, scale);
                }
            }
        }
    }
}
