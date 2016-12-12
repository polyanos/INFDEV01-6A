using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using AssignmentCode;

namespace AssignmentCodeTests
{
    [TestClass]
    public class SearchTests
    {
        [TestMethod]
        public void searhcRange()
        {
            KDVectorNode testTree = new KDVectorNode(null, new Vector2(5, 6), Dimension.X);
            testTree.LeftNode = new KDVectorNode(testTree, new Vector2(3, 3), Dimension.Y);
            testTree.LeftNode.LeftNode = new KDVectorNode(testTree.LeftNode, new Vector2(1, 1), Dimension.X);
            testTree.LeftNode.RightNode = new KDVectorNode(testTree.LeftNode, new Vector2(2, 5), Dimension.X);
            testTree.RightNode = new KDVectorNode(testTree, new Vector2(6, 9), Dimension.Y);
            testTree.RightNode.LeftNode = new KDVectorNode(testTree.RightNode, new Vector2(9, 8), Dimension.Y);

            float beginX = 2;
            float endX = 6;
            float beginY = 5;
            float endY = 9;
        }
    }
}
