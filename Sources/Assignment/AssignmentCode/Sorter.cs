using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssignmentCode
{
    static class Sorter
    {
        static public IEnumerable<Vector2> sortBuildings(Vector2 house, IEnumerable<Vector2> specialBuildings)
        {
            List<Tuple<Vector2, float>> distanceList = new List<Tuple<Vector2, float>>(specialBuildings.Count());
            foreach(Vector2 sb in specialBuildings)
            {
                distanceList.Add(new Tuple<Vector2, float>(sb, calculateDistance(house, sb)));
            }

            return null;
        }

        static private float calculateDistance(Vector2 sp, Vector2 ep)
        {
            return (float)Math.Sqrt(Math.Pow(sp.X - ep.X, 2) + Math.Pow(sp.Y - ep.Y, 2));
        }
    }
}
