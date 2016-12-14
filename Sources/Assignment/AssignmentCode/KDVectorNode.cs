using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssignmentCode
{
    public class KDVectorNode : KDNode<Vector2>
    {
        public Dimension Dimension { get; }
        public KDNode<Vector2> LeftChild { get; set; }
        public KDNode<Vector2> Parent { get; }
        public KDNode<Vector2> RightChild { get; set; }
        public Vector2 Value { get; }

        public KDVectorNode(KDNode<Vector2> parent, Dimension dimension, Vector2 value)
        {
            Parent = parent;
            Dimension = dimension;
            Value = value;
        }

        public bool HasLeftChild()
        {
            return LeftChild != null;
        }

        public bool HasRightChild()
        {
            return RightChild != null;
        }
    }
}
