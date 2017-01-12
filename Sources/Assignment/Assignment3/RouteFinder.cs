using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment3
{
    public class RouteFinder
    {
        public static IEnumerable<Tuple<Vector2, Vector2>> FindRoute(Vector2 startPoint, Vector2 endPoint, IEnumerable<Tuple<Vector2, Vector2>> roadData)
        {
            Graph<VectorVertex, VectorEdge> graph = VectorGraph.CreateGraphFromStreetData(roadData);
            VectorVertex start = new VectorVertex(startPoint);
            VectorVertex end = new VectorVertex(endPoint);

            VectorVertex currentNode;
            Dictionary<VectorVertex, ISet<VectorEdge>> edges = new Dictionary<VectorVertex, ISet<VectorEdge>>(graph.Edges);

            Dictionary<string, VectorVertex> remainingNodes = new Dictionary<string, VectorVertex>(graph.Vertexes);
            Dictionary<string, VectorVertex> processedNodes = new Dictionary<string, VectorVertex>(graph.Vertexes.Count);

            foreach (var vertex in remainingNodes.Values)
            {
                vertex.Distance = double.PositiveInfinity;
            }

            remainingNodes[start.Name].Distance = 0;

            currentNode = start;
            while (!currentNode.Equals(end))
            {
                currentNode = remainingNodes.OrderBy(n=>n.Value.Distance).FirstOrDefault(n=>n.Value.Distance != double.PositiveInfinity).Value;

                if (currentNode != null)
                {
                    foreach (var edge in edges[currentNode])
                    {
                        if (remainingNodes.ContainsKey(edge.End.Name))
                        {
                            var adjacendNode = remainingNodes[edge.End.Name];
                            var newDistance = currentNode.Distance + edge.GetWeight();

                            if (newDistance < adjacendNode.Distance)
                            {
                                adjacendNode.Distance = newDistance;
                                adjacendNode.Previous = currentNode;
                            }
                        }
                    }
                    remainingNodes.Remove(currentNode.Name);
                    processedNodes.Add(currentNode.Name, currentNode);
                }
            }

            currentNode = processedNodes[end.Name];
            List<Tuple<Vector2, Vector2>> pathList = new List<Tuple<Vector2, Vector2>>();
            while (!currentNode.Equals(start))
            {
                pathList.Add(new Tuple<Vector2, Vector2>(currentNode.Previous.Value, currentNode.Value));
                currentNode = currentNode.Previous;
            }
            pathList.Reverse();
            return pathList;
        }
    }
}
