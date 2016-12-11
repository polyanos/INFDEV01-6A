using AssignmentCode.Extensions;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssignmentCode
{
    public static class KDTreeFactory
    {
        public static KDNode CreateTree(IEnumerable<Vector2> points)
        {
            IList<Vector2> vectorList = new List<Vector2>(points);

            KDNode treeRoot = CreateNode(vectorList, Dimension.X);
            return treeRoot;
        }

        private static KDNode CreateNode(IList<Vector2> points, Dimension dimension)
        {
            //Sort the buildings int the current dimension
            points = Sorter.sortBuildingsByDimension(points, dimension);
            //Create the root node.
            KDNode root = new KDNode(getMedianPoint(points), dimension);
            //Divide the remaining points
            var childrenArrays = getLeftRightChildren(points);

            //Create the left and right nodes
            if (childrenArrays.Item1.Count > 0)
            {
                root.LeftNode = CreateNode(childrenArrays.Item1, nextDimension(root.dimension));
            }
            if (childrenArrays.Item2.Count > 0)
            {
                root.RightNode = CreateNode(childrenArrays.Item2, nextDimension(root.dimension));
            }

            return root;
        }

        private static Dimension nextDimension(Dimension dimension)
        {
            switch (dimension)
            {
                case Dimension.X:
                    return Dimension.Y;
                case Dimension.Y:
                    return Dimension.X;
                default:
                    return Dimension.X;
            }
        }

        private static Vector2 getMedianPoint(IList<Vector2> sortedPointList)
        {
            return sortedPointList[sortedPointList.Count / 2];
        }

        private static Tuple<IList<Vector2>, IList<Vector2>> getLeftRightChildren(IList<Vector2> sortedPointList)
        {
            int middle = sortedPointList.Count / 2;
            int last = sortedPointList.Count;

            //Copy the values before of the median
            IList<Vector2> leftList;
            if (middle > 0)
            {
                leftList = sortedPointList.getRange(0, middle-1);
            }
            else
            {
                leftList = new List<Vector2>(0);
            }

            //Copy the values after the median
            IList<Vector2> rightList;
            if ((last - middle) > 1)
            {
                rightList = sortedPointList.getRange(middle + 1, last -1);
            }
            else
            {
                rightList = new List<Vector2>(0);
            }

            return new Tuple<IList<Vector2>, IList<Vector2>>(leftList, rightList);
        }
    }
}
