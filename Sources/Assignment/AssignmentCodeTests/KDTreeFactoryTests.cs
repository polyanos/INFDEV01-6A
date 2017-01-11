using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Assignment1;

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
            KDNode<Vector2> expectedTree = new KDVectorNode(null, Dimension.X, new Vector2(5, 6));
            expectedTree.LeftChild = new KDVectorNode(expectedTree, Dimension.Y, new Vector2(3, 3));
            expectedTree.LeftChild.LeftChild = new KDVectorNode(expectedTree.LeftChild, Dimension.X, new Vector2(1, 1));
            expectedTree.LeftChild.RightChild = new KDVectorNode(expectedTree.LeftChild, Dimension.X, new Vector2(2, 5));
            expectedTree.RightChild = new KDVectorNode(expectedTree, Dimension.Y, new Vector2(6, 9));
            expectedTree.RightChild.LeftChild = new KDVectorNode(expectedTree.RightChild, Dimension.Y, new Vector2(9, 8));

            //Act
            KDNode<Vector2> resultTree = factory.CreateTree(specialBuildings);

            //Assert
            checkNode(expectedTree, resultTree);
        }

        private void checkNode(KDNode<Vector2> expected, KDNode<Vector2> result)
        {
            Assert.IsTrue(expected.Value.X == result.Value.X && expected.Value.Y == result.Value.Y);
            if(expected.LeftChild != null)
            {
                checkNode(expected.LeftChild, result.LeftChild);
            }
            if(expected.RightChild != null)
            {
                checkNode(expected.RightChild, result.RightChild);
            }
        }
    }
}
