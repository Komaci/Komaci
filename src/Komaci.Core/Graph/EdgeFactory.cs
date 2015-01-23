using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Komaci.Core.Graph
{
    public class EdgeFactory<V, E> : IEdgeFactory<V, E>
        where E : IEdge<V>, new()
    {
        public E CreateEdge(V sourceVertex, V targetVertex)
        {
            E egde = new E();
            egde.Source = sourceVertex;
            egde.Target = targetVertex;

            return egde;
        }
    }
}
