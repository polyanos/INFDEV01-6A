using Helper.Extensions;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment3
{
    public class VectorGraph : Graph<Vector2, double>
    {
        public IDictionary<Vector2, ISet<Edge<Vector2, double>>> GraphData { get; private set; }

        private VectorGraph(IDictionary<Vector2, ISet<Edge<Vector2, double>>> data)
        {
            GraphData = data;
        }

        public static Graph<Vector2, double> CreateGraphFromStreetData(IEnumerable<Tuple<Vector2, Vector2>> roadData)
        {
            IDictionary<Vector2, ISet<Edge<Vector2, double>>> graphData = new Dictionary<Vector2, ISet<Edge<Vector2, double>>>(roadData.Count());
            foreach(Tuple<Vector2, Vector2> road in roadData)
            {
                if (!graphData.Keys.Contains(road.Item1))
                {
                    graphData.Add(road.Item1, new HashSet<Edge<Vector2, double>>());
                }
                graphData[road.Item1].Add(new VectorEdge(road.Item1, road.Item2));
            }

            VectorGraph graph = new VectorGraph(graphData);
            return graph;
        }
    }
}
