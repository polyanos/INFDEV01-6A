using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssignmentCode
{
    class Searcher
    {
        public IEnumerable<IEnumerable<Vector2>> searchBuildingsWithinDistance(IEnumerable<Vector2> specialBuildings, IEnumerable<Tuple<Vector2, float>> housesWithDistance)
        {
            KDNode specialBuildingTree = KDTreeFactory.CreateTree(specialBuildings);


        }

        private IEnumerable<Vector2> searchBuildings(KDNode buildingTree, Vector2 building, float distanceFromBuilding)
        {
            float beginX = building.X - distanceFromBuilding;
            float endX = building.X + distanceFromBuilding;
            float beginY = building.Y - distanceFromBuilding;
            float endY = building.Y + distanceFromBuilding;
        }
    }
}
