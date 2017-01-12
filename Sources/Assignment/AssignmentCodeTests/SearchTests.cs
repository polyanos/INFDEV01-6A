using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Assignment2;
using Assignment1;
using System.Linq;

namespace AssignmentCodeTests
{
    [TestClass]
    public class SearchTests
    {
        [TestMethod]
        public void searchRange()
        {
            //Arrange
            KDNode<Vector2> testTree = new KDVectorNode(null, Dimension.X, new Vector2(5, 6));
            testTree.LeftChild = new KDVectorNode(testTree, Dimension.Y, new Vector2(3, 3));
            testTree.LeftChild.LeftChild = new KDVectorNode(testTree.LeftChild, Dimension.X, new Vector2(1, 1));
            testTree.LeftChild.RightChild = new KDVectorNode(testTree.LeftChild, Dimension.X, new Vector2(2, 5));
            testTree.RightChild = new KDVectorNode(testTree, Dimension.Y, new Vector2(6, 9));
            testTree.RightChild.LeftChild = new KDVectorNode(testTree.RightChild, Dimension.Y, new Vector2(9, 8));

            Vector2 testHouse = new Vector2(3, 3);
            float range = 3;
            List<Vector2> expectedResults = new List<Vector2> { new Vector2(3, 3), new Vector2(1, 1), new Vector2(2,5) };

            //Act
            VectorTreeSearcher vts = new VectorTreeSearcher(testTree);
            var returnedValue = vts.SearchBuildingsWithinDistance(new List<Tuple<Vector2, float>> { new Tuple<Vector2, float>(testHouse, range) });
            var enumm = returnedValue.GetEnumerator();
            enumm.MoveNext();
            var result = enumm.Current;

            //Assert
            Assert.AreEqual(expectedResults.Count, result.Count());

            foreach(Vector2 resultVector in result)
            {
                Assert.IsTrue(expectedResults.Exists(v=>v.X == resultVector.X && v.Y == resultVector.Y));
            }
        }
    }
}
