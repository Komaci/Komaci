using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Komaci.Core
{
    public interface IVertexFactory<V>
    {
        V CreateVertex();
    }
}
