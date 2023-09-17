using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;

namespace Physics
{
    public static class Collisions
    {
        public static bool CirclesCollision(CircleBody circle1, CircleBody circle2, out Vector2 normal)
        {
            normal = new Vector2();
            if ((circle1.position - circle2.position).Length() < (circle1.diameter + circle2.diameter) / 2)
            {
                normal.Normalize(circle1.position - circle2.position);
                return true;
            }
            return false;
        }

        public static bool IntersectCirclePolygon(Vector2 circlePosition, float radius, Vector2[] vertcies, out Vector2 normal, out float depth)
        {
            normal = new Vector2(0, 0);
            depth = float.MaxValue;
            Vector2 axis;
            float axisdepth, minA, maxA, minB, maxB;

            for (int i = 0; i < vertcies.Length; i++)
            {
                Vector2 va = vertcies[i];
                Vector2 vb = vertcies[(i + 1) % vertcies.Length];

                Vector2 edge = vb - va;
                axis = new Vector2(-edge.Y, edge.X);
                axis.Normalize(axis);

                ProjectVertcies(vertcies, axis, out minA, out maxA);
                ProjectCircle(circlePosition, radius, axis, out minB, out maxB);

                if (minA >= maxB || minB >= maxA)
                {
                    return false;
                }

                axisdepth = (maxB - minA) < (maxA - minB) ? (maxB - minA) : (maxA - minB);

                if (axisdepth < depth)
                {
                    depth = axisdepth;
                    normal = axis;
                }
            }

            int cpindex = FindClosestPoint(circlePosition, vertcies);
            Vector2 cp = vertcies[cpindex];

            axis = cp - circlePosition;
            axis.Normalize(axis);

            ProjectVertcies(vertcies, axis, out minA, out maxA);
            ProjectCircle(circlePosition, radius, axis, out minB, out maxB);

            if (minA >= maxB || minB >= maxA)
            {
                return false;
            }

            axisdepth = (maxB - minA) < (maxA - minB) ? (maxB - minA) : (maxA - minB);

            if (axisdepth < depth)
            {
                depth = axisdepth;
                normal = axis;
            }


            Vector2 center = FindArithmeticMean(vertcies);

            Vector2 direction = center - circlePosition;

            if (normal.DotProduct(direction) < 0)
            {
                normal = -normal;
            }

            return true;
        }

        private static int FindClosestPoint(Vector2 circlePosition, Vector2[] vertcies)
        {
            int result = -1;
            float minDistance = float.MaxValue;

            for (int i = 0; i < vertcies.Length; i++)
            {
                Vector2 v = vertcies[i];
                float distance = (float)(v - circlePosition).Length();

                if (distance < minDistance)
                {
                    minDistance = distance;
                    result = i;
                }
            }
            return result;
        }

        private static void ProjectCircle(Vector2 circlePosition, float radius, Vector2 axis, out float min, out float max)
        {
            Vector2 direction = new Vector2(0, 0);
            direction.Normalize(axis);

            Vector2 p1 = circlePosition + direction.Scale(radius);
            Vector2 p2 = circlePosition - direction.Scale(radius);

            min = (float)axis.DotProduct(p1);
            max = (float)axis.DotProduct(p2);

            if (min > max)
            {
                float temp = min;
                min = max;
                max = temp;
            }
        }

        public static bool IntersectPolygons(Vector2[] vertciesA, Vector2[] vertciesB, out Vector2 normal, out float depth)
        {
            normal = new Vector2(0, 0);
            depth = float.MaxValue;

            for (int i = 0; i < vertciesA.Length; i++)
            {
                Vector2 va = vertciesA[i];
                Vector2 vb = vertciesA[(i + 1) % vertciesA.Length];

                Vector2 edge = vb - va;
                Vector2 axis = new Vector2(-edge.Y, edge.X);
                axis.Normalize(axis);

                ProjectVertcies(vertciesA, axis, out float minA, out float maxA);
                ProjectVertcies(vertciesB, axis, out float minB, out float maxB);

                if (minA >= maxB || minB >= maxA)
                {
                    return false;
                }

                float axisdepth = (maxB - minA) < (maxA - minB) ? (maxB - minA) : (maxA - minB);

                if (axisdepth < depth)
                {
                    depth = axisdepth;
                    normal = axis;
                }
            }
            for (int i = 0; i < vertciesB.Length; i++)
            {
                Vector2 va = vertciesB[i];
                Vector2 vb = vertciesB[(i + 1) % vertciesB.Length];

                Vector2 edge = vb - va;
                Vector2 axis = new Vector2(-edge.Y, edge.X);
                axis.Normalize(axis);

                ProjectVertcies(vertciesA, axis, out float minA, out float maxA);
                ProjectVertcies(vertciesB, axis, out float minB, out float maxB);

                if (minA >= maxB || minB >= maxA)
                {
                    return false;
                }

                float axisdepth = (maxB - minA) < (maxA - minB) ? (maxB - minA) : (maxA - minB);

                if (axisdepth < depth)
                {
                    depth = axisdepth;
                    normal = axis;
                }
            }

            Vector2 centerA = FindArithmeticMean(vertciesA);
            Vector2 centerB = FindArithmeticMean(vertciesB);

            Vector2 direction = centerB - centerA;

            if (normal.DotProduct(direction) < 0)
            {
                normal = -normal;
            }

            return true;
        }

        public static Vector2 FindArithmeticMean(Vector2[] vertcies)
        {
            float sumX = 0;
            float sumY = 0;

            for (int i = 0; i < vertcies.Length; ++i)
            {
                Vector2 v = vertcies[i];
                sumX += (float)v.X;
                sumY += (float)v.Y;
            }
            return new Vector2(sumX / (float)vertcies.Length, sumY / (float)vertcies.Length);
        }
        private static void ProjectVertcies(Vector2[] vertcies, Vector2 axis, out float min, out float max)
        {
            min = float.MaxValue;
            max = float.MinValue;

            for (int i = 0; i < vertcies.Length; i++)
            {
                Vector2 v = vertcies[i];
                float proj = (float)v.DotProduct(axis);

                if (proj < min)
                {
                    min = proj;
                }
                if (proj > max)
                {
                    max = proj;
                }
            }
        }
        public static bool IsDotInside(Vector2[] vertciesA, Vector2 dot)
        {
            for (int i = 0; i < vertciesA.Length; i++)
            {
                Vector2 va = vertciesA[i];
                Vector2 vb = vertciesA[(i + 1) % vertciesA.Length];

                Vector2 edge = vb - va;
                Vector2 axis = new Vector2(-edge.Y, edge.X);
                axis.Normalize(axis);

                ProjectVertcies(vertciesA, axis, out float minA, out float maxA);

                float proj = (float)dot.DotProduct(axis);

                if (minA >= proj || proj >= maxA)
                {
                    return false;
                }
            }
            return true;
        }

        public static void ResolveCollision(Body body1, Body body2, Vector2 normal)
        {
            Vector2 relativeVelocity = body2.linearVelocity - body1.linearVelocity;

            double e = PhysicsMath.Min(body1.restitution, body2.restitution);

            double j = -(1f + e) * relativeVelocity.DotProduct(normal) / ((1f / body1.mass) + (1f / body2.mass));

            body1.linearVelocity -= j / body1.mass * normal;
            body2.linearVelocity += j / body2.mass * normal;
        }
    }
}
