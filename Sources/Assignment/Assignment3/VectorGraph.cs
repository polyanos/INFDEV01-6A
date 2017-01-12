using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment3
{
    class VectorGraph : Graph<Vector2, double>
    {
        public ISet<Edge<Vector2, double>> Edges { get; private set; }
        public ISet<Vertex<Vector2>> Vertexes { get; private set; }

        private VectorGraph(ISet<Edge<Vector2, double>> Edges, ISet<Vertex<Vector2>> Vertexes)
        {
            this.Edges = Edges;
            this.Vertexes = Vertexes;
        }

        public Dictionary<Vertex<Vector2>, Edge<Vector2, double>> GetAdjacencyList()
        {
            throw new NotImplementedException();
        }

        public static Graph<Vector2, double> CreateGraphFromStreetData()
        {

        }
    }
}
