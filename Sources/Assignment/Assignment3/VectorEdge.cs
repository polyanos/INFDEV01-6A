using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Helper.Extensions;

namespace Assignment3
{
    public class VectorEdge : Edge<Vertex<Vector2, double>, double>
    {
        private double distance;
        public Vertex<Vector2, double> End { get; private set; }
        public Vertex<Vector2, double> Start { get; private set; }

        public VectorEdge(Vertex<Vector2, double> startPoint, Vertex<Vector2, double> endPoint)
        {
            Start = startPoint;
            End = endPoint;
            distance = Start.Value.EuclideanDistance(End.Value);
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
                hash = (hash * 16777619) ^ End.Value.GetBetterHashcode();
                hash = (hash * 16777619) ^ Start.Value.GetBetterHashcode();
                return hash;
            }
        }

        public override bool Equals(object obj)
        {
            VectorEdge e = obj as VectorEdge;
            return e != null 
                && e.GetHashCode() == this.GetHashCode()
                &&((e.Start.Value.X == this.Start.Value.X && e.Start.Value.Y == this.Start.Value.Y) && (e.End.Value.X == this.End.Value.X && e.End.Value.Y == this.End.Value.Y));
        }
    }
}
