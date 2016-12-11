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
        public Dimension dimension { get; }
        public Vector2 point { get; }
        public KDNode LeftNode { set; get; }
        public KDNode RightNode { set; get; }

        public KDNode(Vector2 point, Dimension dimension)
        {
            this.point = point;
            this.dimension = dimension;
        }
    }
}
