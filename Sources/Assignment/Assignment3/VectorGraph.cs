using Helper.Extensions;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment3
{
    public class VectorGraph : Graph<VectorVertex, VectorEdge>
    {
        public IDictionary<VectorVertex, ISet<VectorEdge>> Edges { get; private set; }

        public IDictionary<string, VectorVertex> Vertexes { get; private set; }

        public VectorGraph(IDictionary<VectorVertex, ISet<VectorEdge>> edges, IDictionary<string, VectorVertex> vertexes)
        {
            Edges = edges;
            Vertexes = vertexes;
        }

        public static Graph<VectorVertex, VectorEdge> CreateGraphFromStreetData(IEnumerable<Tuple<Vector2, Vector2>> roadData)
        {
            var edges = new Dictionary<VectorVertex, ISet<VectorEdge>>();
            var vertexes = new Dictionary<string, VectorVertex>();
            string name;

            foreach(var road in roadData)
            {
                name = road.Item1.GetVectorName();
                if (!vertexes.Keys.Contains(name))
                {
                    vertexes.Add(name, new VectorVertex(road.Item1));
                }
            }

            foreach(var road in roadData)
            {
                var source = vertexes[road.Item1.GetVectorName()];
                var edge = new VectorEdge(source, vertexes[road.Item2.GetVectorName()]);

                if (!edges.Keys.Contains(source)) {
                    edges.Add(source, new HashSet<VectorEdge>());
                }
                edges[source].Add(edge);
            }

            VectorGraph graph = new VectorGraph(edges, vertexes);
            return graph;
        }
    }
}
