using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using AssignmentCode;

namespace AssignmentCodeTests
{
    [TestClass]
    public class KDTreeFactoryTests
    {
        [TestMethod]
        public void CreateTreeTest()
        {
            //Arange
            KDVectorTreeFactory factory = new KDVectorTreeFactory();
            IEnumerable<Vector2> specialBuildings = new List<Vector2>() { new Vector2(5, 6), new Vector2(6, 9), new Vector2(3, 3), new Vector2(9, 8), new Vector2(1, 1), new Vector2(2, 5) };
            KDVectorNode expectedTree = new KDVectorNode(null, new Vector2(5, 6), Dimension.X);
            expectedTree.LeftNode = new KDVectorNode(expectedTree, new Vector2(3, 3), Dimension.Y);
            expectedTree.LeftNode.LeftNode = new KDVectorNode(expectedTree.LeftNode, new Vector2(1, 1), Dimension.X);
            expectedTree.LeftNode.RightNode = new KDVectorNode(expectedTree.LeftNode, new Vector2(2, 5), Dimension.X);
            expectedTree.RightNode = new KDVectorNode(expectedTree, new Vector2(6, 9), Dimension.Y);
            expectedTree.RightNode.LeftNode = new KDVectorNode(expectedTree.RightNode, new Vector2(9, 8), Dimension.Y);

            //Act
            KDVectorNode resultTree = factory.CreateTree(specialBuildings);

            //Assert
            checkNode(expectedTree, resultTree);
        }

        private void checkNode(KDVectorNode expected, KDVectorNode result)
        {
            Assert.IsTrue(expected.Point.X == result.Point.X && expected.Point.Y == result.Point.Y);
            if(expected.LeftNode != null)
            {
                checkNode(expected.LeftNode, result.LeftNode);
            }
            if(expected.RightNode != null)
            {
                checkNode(expected.RightNode, result.RightNode);
            }
        }
    }
}
