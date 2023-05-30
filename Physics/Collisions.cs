using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Physics
{
    public static class Collisions
    {
        public static bool CirclesCollision(CircleBody circle1, CircleBody circle2)
        {
            if ((circle1.position - circle2.position).Length() < (circle1.diameter + circle2.diameter) / 2)
            {
                Vector2 normal1 = new Vector2();
                normal1.Normalize(circle1.position - circle2.position);
                Vector2 normal2 = new Vector2();
                normal2.Normalize(circle2.position - circle1.position);
                double overlap = (circle1.diameter + circle2.diameter) / 2 - (circle1.position - circle2.position).Length();
                circle1.position += normal1.Scale((overlap) / 2);
                circle2.position += normal2.Scale((overlap) / 2);
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool IntersectPolygons(Vector2[] vertciesA, Vector2[] vertciesB)
        {
            for(int i = 0; i < vertciesA.Length; i++)
            {
                Vector2 va = vertciesA[i];
                Vector2 vb = vertciesA[(i + 1) % vertciesA.Length];

                Vector2 edge = vb - va;
                Vector2 axis = new Vector2(-edge.Y, edge.X);
                ProjectVertcies(vertciesA, axis, out float minA, out float maxA);
                ProjectVertcies(vertciesB, axis, out float minB, out float maxB);

                if(minA >= maxB || minB >= maxA)
                {
                    return false;
                }
            }
            for (int i = 0; i < vertciesB.Length; i++)
            {
                Vector2 va = vertciesB[i];
                Vector2 vb = vertciesB[(i + 1) % vertciesB.Length];

                Vector2 edge = vb - va;
                Vector2 axis = new Vector2(-edge.Y, edge.X);
                ProjectVertcies(vertciesA, axis, out float minA, out float maxA);
                ProjectVertcies(vertciesB, axis, out float minB, out float maxB);

                if (minA >= maxB || minB >= maxA)
                {
                    return false;
                }
            }
            return true;
        }
        private static void ProjectVertcies(Vector2[] vertcies,Vector2 axis, out float min, out float max)
        {
            min = float.MaxValue;
            max = float.MinValue;

            for (int i = 0; i < vertcies.Length; i++)
            {
                Vector2 v = vertcies[i];
                float proj = (float)v.DotProduct(axis);
                
                if(proj < min)
                {
                    min = proj;
                }
                if(proj > max)
                {
                    max = proj;
                } 
            }
        }
    }
}
