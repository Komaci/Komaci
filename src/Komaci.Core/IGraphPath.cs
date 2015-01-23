using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Komaci.Core
{
    public interface IGraphPath<V, E>
        where E : IEdge<V>
    {
        IGraph<V, E> Graph { get; }

        V StartVertex { get;  }

        V EndVertex { get; }

        IEnumerable<E> Edges { get; }

        double Weight { get; }
    }
}
