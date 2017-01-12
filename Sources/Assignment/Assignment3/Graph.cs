using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment3
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T">The datatype of the point contained in the Vertexes</typeparam>
    /// <typeparam name="Y">The datatype of the weight of a Edge</typeparam>
    interface Graph<T,Y>
    {
        ISet<Edge<T, Y>> Edges { get; }
        ISet<Vertex<T>> Vertexes { get; }

        Dictionary<Vertex<T>, Edge<T, Y>> GetAdjacencyList();
    }
}
