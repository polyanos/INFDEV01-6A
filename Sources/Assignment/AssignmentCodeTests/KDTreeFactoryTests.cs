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
            IEnumerable<Vector2> specialBuildings = new List<Vector2>() { new Vector2(5, 6), new Vector2(6, 9), new Vector2(3, 3), new Vector2(9, 8), new Vector2(1, 1), new Vector2(2, 5) };
            KDNode expectedTree = new KDNode(new Vector2(5,6), Dimension.X);
            expectedTree.LeftNode = new KDNode(new Vector2(3,3), Dimension.Y);
            expectedTree.LeftNode.LeftNode = new KDNode(new Vector2(1, 1), Dimension.X);
            expectedTree.LeftNode.RightNode = new KDNode(new Vector2(2, 5), Dimension.X);
            expectedTree.RightNode = new KDNode(new Vector2(6,9), Dimension.Y);
            expectedTree.RightNode.LeftNode = new KDNode(new Vector2(9, 8), Dimension.Y);

            //Act
            KDNode resultTree = KDTreeFactory.CreateTree(specialBuildings);

            //Assert
            checkNode(expectedTree, resultTree);
        }

        private void checkNode(KDNode expected, KDNode result)
        {
            Assert.IsTrue(expected.point.X == result.point.X && expected.point.Y == result.point.Y);
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
