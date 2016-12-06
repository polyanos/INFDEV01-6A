﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssignmentCode
{
    static class Sorter
    {
        /// <summary>
        /// Sorts the distance, ascending, of special buildings relative to the specified house.
        /// </summary>
        /// <param name="house">The house which acts as the starting point.</param>
        /// <param name="specialBuildings">The special buildings which need to be sorted on distance.</param>
        /// <returns>A ordered list of special building vectors in ascending order.</returns>
        static public IEnumerable<Vector2> sortBuildings(Vector2 house, IEnumerable<Vector2> specialBuildings)
        {
            List<Vector2> sortedList = new List<Vector2>(specialBuildings.Count()); 
            List<Tuple<Vector2, float>> distanceList = new List<Tuple<Vector2, float>>(specialBuildings.Count());

            foreach (Vector2 sb in specialBuildings)
            {
                distanceList.Add(new Tuple<Vector2, float>(sb, calculateDistance(house, sb)));
            }

            var result = recursiveSort(distanceList);
            result.ForEach(x => sortedList.Add(x.Item1));

            return sortedList;
        }

        /// <summary>
        /// Calculates the Euclidean distance between the starting point and ending point. 
        /// The distance will be returned as a single precision floating point number.
        /// </summary>
        /// <param name="sp">The starting point.</param>
        /// <param name="ep">The ending point.</param>
        /// <returns>The distance between the two points.</returns>
        static private float calculateDistance(Vector2 sp, Vector2 ep)
        {
            return (float)Math.Sqrt(Math.Pow(sp.X - ep.X, 2) + Math.Pow(sp.Y - ep.Y, 2));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="distanceList"></param>
        /// <returns></returns>
        static private List<Tuple<Vector2, float>> recursiveSort(List<Tuple<Vector2, float>> distanceList)
        {
            if (distanceList.Count() > 1)
            {
                int middle = distanceList.Count() / 2;
                int index = 0, leftIndex = 0, rightIndex = 0;

                List<Tuple<Vector2, float>> left = new List<Tuple<Vector2, float>>(middle);
                List<Tuple<Vector2, float>> right = new List<Tuple<Vector2, float>>(distanceList.Count() - middle);

                for (; index < middle; index++)
                {
                    left.Add(distanceList.ElementAt(index));
                }
                for (; index < distanceList.Count(); index++)
                {
                    right.Add(distanceList.ElementAt(index));
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

                    if (left.ElementAt(leftIndex).Item2 < right.ElementAt(rightIndex).Item2)
                    {
                        distanceList.Add(left.ElementAt(leftIndex));
                        leftIndex++;
                    }
                    else if (left.ElementAt(leftIndex).Item2 > right.ElementAt(rightIndex).Item2)
                    {
                        distanceList.Add(right.ElementAt(rightIndex));
                        rightIndex++;
                    }
                    else
                    {
                        distanceList.Add(left.ElementAt(leftIndex));
                        leftIndex++;
                    }
                }
            }

            return distanceList;
        }

        static private List<Tuple<Vector2, float>> copyRemainingItems(List<Tuple<Vector2, float>> destination, List<Tuple<Vector2, float>> source, int sourceIndex)
        {
            for (; sourceIndex < source.Count(); sourceIndex++)
            {
                destination.Add(source.ElementAt(sourceIndex));
            }

            return destination;
        }
    }
}
