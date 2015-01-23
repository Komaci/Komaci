using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Komaci.Core
{
    public interface IUndirectedGraph<V, E> : IGraph<V, E>
        where E : IEdge<V>
    {
        int DegreeOf(V vertex);
    }
}
