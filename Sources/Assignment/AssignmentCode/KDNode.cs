using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssignmentCode
{
    public class KDNode
    {
        public Dimension Dimension { get; }
        public Vector2 Point { get; }
        public KDNode Parent { get; set; }
        public KDNode LeftNode { get; set; }
        public KDNode RightNode { get; set; }

        public KDNode(KDNode parent, Vector2 point, Dimension dimension)
        {
            Point = point;
            Dimension = dimension;
            Parent = parent;
        }
    }
}
