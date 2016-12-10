using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssignmentCode
{
    class KDNode
    {
        Vector2 point;
        KDNode leftNode;
        KDNode rightNode;

        public KDNode(Vector2 point)
        {
            this.point = point;
        }

        private void setLeftChild(KDNode child)
        {

        }

        private void setRightChild(KDNode child)
        {

        }

        public KDNode CreateTree(ICollection<Vector2> points)
        {
            return ;
        }

        public enum Dimension { X,Y}
    }
}
