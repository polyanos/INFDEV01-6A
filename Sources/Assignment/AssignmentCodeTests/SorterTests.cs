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
            List<Vector2> expectedResult = new List<Vector2>() { new Vector2(1, 5), new Vector2(3, 6), new Vector2(2, 3), new Vector2(6, 6), new Vector2(1, 1), new Vector2(9, 8) };

            //Act
            var result = Sorter.sortBuildings(house, buildings);

            //Assert
            Assert.IsTrue(result.Count() == expectedResult.Count);

            int x = 0;
            Vector2 expectedVector;
            foreach(Vector2 resultVector in result)
            {
                expectedVector = expectedResult[x];
                x++;

                Assert.IsTrue(resultVector.X == expectedVector.X && resultVector.Y == expectedVector.Y);
            }
        }
    }
}
