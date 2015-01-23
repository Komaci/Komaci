using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Komaci.Core
{
    public interface IEdge<V>
    {
        V Source { get; set;  }

        V Target { get; set; }
    }
}
