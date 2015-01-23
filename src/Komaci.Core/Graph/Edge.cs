using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Komaci.Core.Graph
{
    public class Edge<V> : IEdge<V>
    {
        private V _source;
        private V _target;

        public V Source
        {
            get 
            { 
                return _source; 
            }
            set 
            {
                _source = value;
            }
        }

        public V Target
        {
            get
            {
                return _target;
            }
            set
            {
                _target = value;
            }
        }

        public override string ToString()
        {
            return "(" + _source + " : " + _target + ")";
        }
    }
}
