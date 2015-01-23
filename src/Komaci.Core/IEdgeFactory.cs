using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Komaci.Core
{
    public interface IEdgeFactory<V, E>
        where E : IEdge<V>
    {
        E CreateEdge(V sourceVertex, V targetVertex);
    }
}
