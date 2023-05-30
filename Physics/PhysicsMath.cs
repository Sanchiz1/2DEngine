using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Physics
{
    public static class PhysicsMath
    {
        public static int Min(int a, int b)
        {
            return a < b ? a : b;
        }
        public static float Min(float a, float b)
        {
            return a < b ? a : b;
        }
    }
}
