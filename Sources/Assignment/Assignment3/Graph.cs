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
    public interface Graph<T, Y>
    {
        IDictionary<T, ISet<Edge<T,Y>>> GraphData { get; }
    }
}
