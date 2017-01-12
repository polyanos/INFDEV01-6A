using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment3
{
    class VectorVertex : Vertex<Vector2>
    {
        public Vector2 Id { get; private set; }

        public VectorVertex(Vector2 Id)
        {
            this.Id = Id;
        }

        public override int GetHashCode()
        {
            return new { Id.X, Id.Y }.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            VectorVertex v = obj as VectorVertex;
            return v != null && v.GetHashCode() == this.GetHashCode();
        }
    }
}
