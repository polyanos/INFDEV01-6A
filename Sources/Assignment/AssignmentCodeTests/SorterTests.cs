using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AssignmentCode;
using Microsoft.Xna.Framework;

namespace AssignmentCodeTests
{
    /// <summary>
    /// Summary description for SorterTests
    /// </summary>
    [TestClass]
    public class SorterTests
    {
        [TestMethod]
        public void sortTest()
        {
            //Arrange
            Vector2 house = new Vector2(2, 5);
            List<Vector2> buildings = new List<Vector2>() { new Vector2(3, 6), new Vector2(6, 6), new Vector2(2, 3), new Vector2(9, 8), new Vector2(1, 1), new Vector2(1, 5)};
            List<Vector2> expectedResultList = new List<Vector2>() { new Vector2(1, 5), new Vector2(3, 6), new Vector2(2, 3), new Vector2(6, 6), new Vector2(1, 1), new Vector2(9, 8) };

            //Act
            var resultList = VectorSorter.sortBuildings(house, buildings);

            //Assert
            Assert.IsTrue(resultList.Count() == expectedResultList.Count);

            int x = 0;
            foreach(Vector2 result in resultList)
            {
                Assert.IsTrue(result.X == expectedResultList[x].X && result.Y == expectedResultList[x].Y);
                x++;
            }
        }

        [TestMethod]
        public void sortBuildingsByXDimensionTest()
        {
            //Arrange
            Dimension dimension = Dimension.X;
            IEnumerable<Vector2> specialBuildings = new List<Vector2>() { new Vector2(3, 6), new Vector2(6, 6), new Vector2(2, 3), new Vector2(9, 8), new Vector2(1, 1), new Vector2(1, 5) };
            Vector2[] expectedResult = new Vector2[] { new Vector2(1, 1), new Vector2(1,5), new Vector2(2, 3), new Vector2(3,6), new Vector2(6,6), new Vector2(9, 8) };

            //Act
            var result = VectorSorter.sortBuildingsByDimension(specialBuildings, dimension);

            //Assert
            Assert.IsTrue(result.Count == expectedResult.Length);

            for(int x = 0; x < expectedResult.Length; x++)
            {
                //Only check the relevant dimension as the the order with the same dimensional coordinate is not guaranteed.
                Assert.IsTrue(expectedResult[x].X == result[x].X);
            }
        }

        [TestMethod]
        public void sortBuildingsByYDimensionTest()
        {
            //Arrange
            Dimension dimension = Dimension.Y;
            IEnumerable<Vector2> specialBuildings = new List<Vector2>() { new Vector2(3, 6), new Vector2(6, 6), new Vector2(2, 3), new Vector2(9, 8), new Vector2(1, 1), new Vector2(1, 5) };
            Vector2[] expectedResult = new Vector2[] { new Vector2(1,1), new Vector2(2,3), new Vector2(1,5), new Vector2(3, 6), new Vector2(6, 6), new Vector2(9, 8) };

            //Act
            var result = VectorSorter.sortBuildingsByDimension(specialBuildings, dimension);

            //Assert
            Assert.IsTrue(result.Count == expectedResult.Length);

            for (int x = 0; x < expectedResult.Length; x++)
            {
                //Only check the relevant dimension as the the order within the same dimensional coordinate is not guaranteed.
                Assert.IsTrue(expectedResult[x].Y == result[x].Y);
            }
        }
    }
}
