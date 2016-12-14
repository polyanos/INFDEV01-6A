using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using AssignmentCode.Extensions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssignmentCode
{
    public class VectorTreeSearcher
    {
        private KDNode<Vector2> vectorTree;
        private int nodeTravelCount;
        public VectorTreeSearcher(KDNode<Vector2> vectorTree)
        {
            this.vectorTree = vectorTree;
        }

        public IEnumerable<IEnumerable<Vector2>> SearchBuildingsWithinDistance(IEnumerable<Tuple<Vector2, float>> housesWithDistance)
        {
            IList<IEnumerable<Vector2>> foundBuildingLists = new List<IEnumerable<Vector2>>(10);
            foreach (Tuple<Vector2, float> hwd in housesWithDistance)
            {
                nodeTravelCount = 0;
                var resultList = SearchBuildings(hwd.Item1, hwd.Item2);
                foundBuildingLists.Add(resultList);
                Debug.WriteLine("Searched in " + nodeTravelCount + " nodes.");
                Debug.WriteLine("Found " + resultList.Count + " nodes.");
            }

            return foundBuildingLists;
        }

        private IList<Vector2> SearchBuildings(Vector2 building, float distanceFromBuilding)
        {
            Debug.WriteLine("Searching for buildings in a range of " + distanceFromBuilding + " points, from point " + building.ToPoint());

            SearchInfoContainer sic = new SearchInfoContainer(building, distanceFromBuilding);
            List<Vector2> container = new List<Vector2>(20);

            return FindValues(vectorTree, sic, container);
        }

        private IList<Vector2> FindValues(KDNode<Vector2> node, SearchInfoContainer info, IList<Vector2> foundValues)
        {
            //Debug.WriteLine("Arived at node: "+node.Value.ToString());

            float compareValue;
            float compareBeginValue;
            float compareEndValue;
            if (node.Dimension == Dimension.X)
            {
                compareValue = node.Value.X;
                compareBeginValue = info.BeginX;
                compareEndValue = info.EndX;
            }
            else
            {
                compareValue = node.Value.Y;
                compareBeginValue = info.BeginY;
                compareEndValue = info.EndY;
            }

            if (compareValue > compareBeginValue)
            {
                if (node.HasLeftChild())
                {
                    //Debug.WriteLine("Going to node: " + node.LeftChild.Value.ToString() + " from node " + node.Value.ToString());
                    FindValues(node.LeftChild, info, foundValues);
                }
            }

            if (compareValue < compareEndValue)
            {
                if (node.HasRightChild())
                {
                    //Debug.WriteLine("Going to node: " + node.RightChild.Value.ToString() + " from node " + node.Value.ToString());
                    FindValues(node.RightChild, info, foundValues);
                }
            }

            if ((node.Value.X >= info.BeginX && node.Value.X <= info.EndX) && (node.Value.Y >= info.BeginY && node.Value.Y <= info.EndY))
            {
                Debug.WriteLine("Found possible value at node " + node.Value.ToString());
                if (node.Value.EuclideanDistance(info.Vector) < info.Range)
                {
                    Debug.WriteLine("Found value at node " + node.Value.ToString());
                    foundValues.Add(node.Value);
                }
            }
            nodeTravelCount++;
            return foundValues;
        }


        private class SearchInfoContainer
        {
            public float BeginX { get; private set; }
            public float BeginY { get; private set; }
            public float EndX { get; private set; }
            public float EndY { get; private set; }
            public Vector2 Vector { get; }
            public float Range { get; }

            public SearchInfoContainer(Vector2 startPoint, float range)
            {
                BeginX = startPoint.X - range;
                EndX = startPoint.X + range;
                BeginY = startPoint.Y - range;
                EndY = startPoint.Y + range;
                Vector = startPoint;
                Range = range;
            }
        }
    }
}
