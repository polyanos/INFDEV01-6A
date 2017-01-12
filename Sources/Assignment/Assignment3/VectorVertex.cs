using Helper.Extensions;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment3
{
    public class VectorVertex : Vertex<Vector2, double>
    {
        public double Distance { get; set; }
        public string Name { get; private set; }
        public Vector2 Value { get; private set; }
        public VectorVertex Previous { get; set; }

        public VectorVertex(Vector2 value)
        {
            Name = "X" + value.X + "Y" + value.Y;
            Value = value;
        }

        public override int GetHashCode()
        {
            return Value.GetBetterHashcode();
        }

        public override bool Equals(object obj)
        {
            VectorVertex v = obj as VectorVertex;
            return v != null && v.GetHashCode() == this.GetHashCode() && (v.Value.X == this.Value.X && v.Value.Y == this.Value.Y);
        }
    }
}
