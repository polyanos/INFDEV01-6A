using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssignmentCode
{
    public class KDVectorNode
    {
        public Dimension Dimension { get; }
        public Vector2 Point { get; }
        public KDVectorNode Parent { get; set; }
        public KDVectorNode LeftNode { get; set; }
        public KDVectorNode RightNode { get; set; }

        public KDVectorNode(KDVectorNode parent, Vector2 point, Dimension dimension)
        {
            Point = point;
            Dimension = dimension;
            Parent = parent;
        }
    }
}
