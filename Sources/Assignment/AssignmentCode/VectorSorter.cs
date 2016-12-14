using AssignmentCode.Extensions;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssignmentCode
{
    static public class VectorSorter
    {
        /// <summary>
        /// Sorts the distance, ascending, of special buildings relative to the specified house.
        /// </summary>
        /// <param name="house">The house which acts as the starting point.</param>
        /// <param name="specialBuildings">The special buildings which need to be sorted on distance.</param>
        /// <returns>A ordered list of special building vectors in ascending order.</returns>
        static public IList<Vector2> sortBuildings(Vector2 house, IEnumerable<Vector2> specialBuildings)
        {
            IList<Vector2> sortedList = new List<Vector2>(specialBuildings.Count()); 
            IList<Tuple<Vector2, float>> sortList = new List<Tuple<Vector2, float>>(specialBuildings.Count());

            foreach (Vector2 sb in specialBuildings)
            {
                sortList.Add(new Tuple<Vector2, float>(sb, sb.EuclideanDistance(house)));
            }

            sortList = recursiveSort(sortList);
            foreach(Tuple<Vector2, float> sortItem in sortList)
            {
                sortedList.Add(sortItem.Item1);
            }

            return sortedList;
        }

        static public IList<Vector2> sortBuildingsByDimension(IEnumerable<Vector2> specialBuildings,  Dimension dimension)
        {
            IList<Tuple<Vector2, float>> sortList = new List<Tuple<Vector2, float>>(specialBuildings.Count());
            switch (dimension)
            {
                case Dimension.X:
                    foreach(Vector2 sb in specialBuildings) { sortList.Add(new Tuple<Vector2, float>(sb, sb.X)); }
                    break;
                case Dimension.Y:
                    foreach (Vector2 sb in specialBuildings) { sortList.Add(new Tuple<Vector2, float>(sb, sb.Y)); }
                    break;
            }
            sortList = recursiveSort(sortList);

            IList<Vector2> resultList = new List<Vector2>(sortList.Count);
            foreach(Tuple<Vector2, float> sortItem in sortList)
            {
                resultList.Add(sortItem.Item1);
            }
            return resultList;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="distanceList"></param>
        /// <returns></returns>
        static private IList<Tuple<Vector2, float>> recursiveSort(IList<Tuple<Vector2, float>> distanceList)
        {
            if (distanceList.Count() > 1)
            {
                int middle = distanceList.Count() / 2;
                int index = 0, leftIndex = 0, rightIndex = 0;

                IList<Tuple<Vector2, float>> left = new List<Tuple<Vector2, float>>(middle);
                IList<Tuple<Vector2, float>> right = new List<Tuple<Vector2, float>>(distanceList.Count() - middle);

                for (; index < middle; index++)
                {
                    left.Add(distanceList[index]);
                }
                for (; index < distanceList.Count(); index++)
                {
                    right.Add(distanceList[index]);
                }

                left = recursiveSort(left);
                right = recursiveSort(right);

                int oldCount = distanceList.Count();
                distanceList = new List<Tuple<Vector2, float>>(oldCount);
                while(distanceList.Count() < oldCount)
                {
                    if (leftIndex >= left.Count())
                    {
                        copyRemainingItems(distanceList, right, rightIndex);
                        break;
                    }
                    if (rightIndex >= right.Count())
                    {
                        copyRemainingItems(distanceList, left, leftIndex);
                        break;
                    }

                    if (left[leftIndex].Item2 < right[rightIndex].Item2)
                    {
                        distanceList.Add(left[leftIndex]);
                        leftIndex++;
                    }
                    else if (left[leftIndex].Item2 > right[rightIndex].Item2)
                    {
                        distanceList.Add(right[rightIndex]);
                        rightIndex++;
                    }
                    else
                    {
                        distanceList.Add(left[leftIndex]);
                        leftIndex++;
                    }
                }
            }

            return distanceList;
        }

        static private IList<Tuple<Vector2, float>> copyRemainingItems(IList<Tuple<Vector2, float>> destination, IList<Tuple<Vector2, float>> source, int sourceIndex)
        {
            for (; sourceIndex < source.Count(); sourceIndex++)
            {
                destination.Add(source[sourceIndex]);
            }

            return destination;
        }

        public enum SortDimension { X,Y}
    }
}
