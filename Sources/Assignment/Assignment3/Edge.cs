using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment3
{
    public interface Edge<T, Y>
    {
        T Start { get; }
        T End { get;  }
        Y GetWeight();
    }
}
