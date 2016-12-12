using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssignmentCode
{
    class VectorTreeSearcher
    {
        private KDVectorNode vectorTree;
        public VectorTreeSearcher(KDVectorNode vectorTree)
        {
            this.vectorTree = vectorTree;
        }

        public IEnumerable<IEnumerable<Vector2>> searchBuildingsWithinDistance(IEnumerable<Tuple<Vector2, float>> housesWithDistance)
        {

        }

        private IEnumerable<Vector2> searchBuildings(Vector2 building, float distanceFromBuilding)
        {
            float beginX = building.X - distanceFromBuilding;
            float endX = building.X + distanceFromBuilding;
            float beginY = building.Y - distanceFromBuilding;
            float endY = building.Y + distanceFromBuilding;
        }
    }
}
