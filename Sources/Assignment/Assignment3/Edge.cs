using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment3
{
    interface Edge<T, Y>
    {
        Vertex<T> Start { get; }
        Vertex<T> End { get;  }
        Y GetWeight();
    }
}
