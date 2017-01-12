using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Helper.Extensions;

namespace Assignment3
{
    public class VectorEdge : Edge<Vector2, double>
    {
        private double distance;
        public Vector2 End { get; private set; }
        public Vector2 Start { get; private set; }

        public VectorEdge(Vector2 startPoint, Vector2 endPoint)
        {
            Start = startPoint;
            End = endPoint;
            distance = Start.EuclideanDistance(End);
        }

        public double GetWeight()
        {
            return distance;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = (int)2166136261;
                // Suitable nullity checks etc, of course :)
                hash = (hash * 16777619) ^ End.GetBetterHashcode();
                hash = (hash * 16777619) ^ Start.GetBetterHashcode();
                return hash;
            }
        }

        public override bool Equals(object obj)
        {
            VectorEdge e = obj as VectorEdge;
            return e != null && e.GetHashCode() == this.GetHashCode();
        }
    }
}
