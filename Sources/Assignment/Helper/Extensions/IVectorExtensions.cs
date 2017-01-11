using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper.Extensions
{
    public static class IVectorExtensions
    {
        static public float EuclideanDistance(this Vector2 start, Vector2 end)
        {
            return (float)Math.Sqrt(Math.Pow(start.X - end.X, 2) + Math.Pow(start.Y - end.Y, 2));
        }
    }
}
