using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Komaci.Core
{
    public interface IGraphMapping<V, E>
        where E : IEdge<V>
    {
        V GetVertexCorrespondence(V vertex, bool forward);

        E GetEdgeCorrespondence(E edge, bool forward);
    }
}
