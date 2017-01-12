using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Helper.Extensions;

namespace Assignment3
{
    class VectorEdge : Edge<Vector2, double>
    {
        private double distance;
        public Vertex<Vector2> End { get; private set; }
        public Vertex<Vector2> Start { get; private set; }

        public VectorEdge(Vertex<Vector2> startPoint, Vertex<Vector2> endPoint)
        {
            Start = startPoint;
            End = endPoint;
            distance = Start.Id.EuclideanDistance(End.Id);
        }

        public double GetWeight()
        {
            return distance;
        }

        public override int GetHashCode()
        {
            return new { SX = Start.Id.X, SY = Start.Id.Y, EX = End.Id.X, EY = End.Id.Y }.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            VectorEdge e = obj as VectorEdge;
            return e != null && e.GetHashCode() == this.GetHashCode();
        }
    }
}
