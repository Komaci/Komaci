using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Komaci.Core
{
    public interface IWeightedGraph<V, E> : IGraph<V, E>
        where E : IEdge<V>
    {
        void SetEdgeWeight(E edge, double weight);

        double GetEdgeWeight(E edge);
    }
}
