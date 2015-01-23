using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Komaci.Core
{
    public interface IDirectedGraph<V, E> : IGraph<V, E>
        where E : IEdge<V>
    {
        int InDegreeOf(V vertex);

        IEnumerable<E> IncomingEdgesOf(V vertex);

        int OutDegreeOf(V vertex);

        IEnumerable<E> OutgoingEdgesOf(V vertex);
    }
}
