using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Komaci.Core
{
    public interface IMultiGraph<V, E> : IGraph<V, E>
        where E : IEdge<V>
    {
        IEnumerable<E> GetEdges(V sourceVertex, V targetVertex);

        IEnumerable<E> RemoveEdges(V sourceVertex, V targetVertex);
    }
}
