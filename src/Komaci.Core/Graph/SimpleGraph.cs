using Komaci.Core.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Komaci.Core.Graph
{
    //TODO::implement IClonable
    public class SimpleGraph<V, E> : IGraph<V, E>, IUndirectedGraph<V, E>
        where E : IEdge<V>, new()
    {
        private IEdgeFactory<V, E> _edgeFactory;
        private ISet<E> _edges;
        private IDictionary<V, ISet<E>> _vertexEdgeMap;

        // TODO::consider keeping intrusive edge dictionary instead of edge set
        //private IDictionary<E, E> edgeMap;

        public SimpleGraph() : this(new EdgeFactory<V, E>())
        {}

        public SimpleGraph(IEdgeFactory<V, E> edgeFactory)
        {
            if (edgeFactory == null)
            {
                throw new ArgumentNullException("edgeFactory");
            }

            _edgeFactory = edgeFactory;
            _vertexEdgeMap = new Dictionary<V, ISet<E>>();
            _edges = new HashSet<E>();
        }

        #region IGraph
        public bool LoopsAllowed
        {
            get { return false; }
        }

        public bool MultipleEdgesAllowed
        {
            get { return false; }
        }

        public IEdgeFactory<V, E> EdgeFactory
        {
            get { return _edgeFactory; }
        }

        public IEnumerable<E> Edges
        {
            get { return _edges; }
        }

        public IEnumerable<V> Vertices
        {
            get { return _vertexEdgeMap.Keys; }
        }

        public E GetEdge(V sourceVertex, V targetVertex)
        {
            if (ContainsVertex(sourceVertex)
                && ContainsVertex(targetVertex))
            {
                ISet<E> sourceVertexEdges;
                if (_vertexEdgeMap.TryGetValue(sourceVertex, out sourceVertexEdges))
                {
                    if (sourceVertexEdges != null)
                    { 
                        foreach (var edge in sourceVertexEdges)
                        {
                            if (IsAdjucent(sourceVertex, targetVertex, edge))
                            {
                                return edge;
                            }
                        }
                    }
                }
            }

            return default(E);
        }

        public E AddEdge(V sourceVertex, V targetVertex)
        {
            AssertVertexExist(sourceVertex);
            AssertVertexExist(targetVertex);

            // TODO::review, since ContainsEdge calles GetEdge which calls ContainsVertex, 
            //      and we have called AssertVertexExist already
            if (ContainsEdge(sourceVertex, targetVertex))
            {
                return default(E);
            }

            if (sourceVertex.Equals(targetVertex))
            {
                throw new ArgumentException(ErrorMessages.LoopsNotAllowed);
            }

            var edge = _edgeFactory.CreateEdge(sourceVertex, targetVertex);
            
            _edges.Add(edge);

            if (_vertexEdgeMap[sourceVertex] == null)
            {
                _vertexEdgeMap[sourceVertex] = new HashSet<E>();
            }
            _vertexEdgeMap[sourceVertex].Add(edge);

            if (_vertexEdgeMap[targetVertex] == null)
            {
                _vertexEdgeMap[targetVertex] = new HashSet<E>();
            }
            _vertexEdgeMap[targetVertex].Add(edge);

            return edge;
        }

        public bool AddEdge(V sourceVertex, V targetVertex, E edge)
        {
            if (edge == null)
            {
                throw new ArgumentNullException("edge");
            }
            else if (ContainsEdge(edge))
            {
                return false;
            }

            AssertVertexExist(sourceVertex);
            AssertVertexExist(targetVertex);

            if (sourceVertex.Equals(targetVertex))
            {
                throw new ArgumentException(ErrorMessages.LoopsNotAllowed);
            }

            _edges.Add(edge);

            AddEdgeToTouchingVertices(edge);

            return true;
        }

        public bool AddVertex(V vertex)
        {
            if (vertex == null)
            {
                throw new ArgumentNullException("vertex");
            }
            else if (ContainsVertex(vertex))
            {
                return false;
            }
            else
            {
                _vertexEdgeMap.Add(vertex, null);

                return true;
            }
        }

        // TODO::review this as for undirected graphs source and target can be a bit tricky
        public bool ContainsEdge(V sourceVertex, V targetVertex)
        {
            // TODO::optimize this since vertex check will be twice
            var edge = GetEdge(sourceVertex, targetVertex);
            return edge != null && !edge.Equals(default(E));
        }

        public bool ContainsEdge(E edge)
        {
            return _edges.Contains(edge);
        }

        public bool ContainsVertex(V vertex)
        {
            return  _vertexEdgeMap.ContainsKey(vertex);
        }

        public IEnumerable<E> EdgesOf(V vertex)
        {
            return _vertexEdgeMap[vertex];
        }

        public bool RemoveEdges(IEnumerable<E> edges)
        {
            bool modified = false;

            foreach (var edge in edges) 
            {
                modified |= RemoveEdge(edge);
            }

            return modified;
        }

        public E RemoveEdge(V sourceVertex, V targetVertex)
        {
            var edge = GetEdge(sourceVertex, targetVertex);

            if (edge != null && !edge.Equals(default(E)))
            {
                RemoveEdgeFromTouchingVertices(edge);
                _edges.Remove(edge);
            }

            return edge;
        }

        public bool RemoveEdge(E edge)
        {
            if (ContainsEdge(edge))
            {
                RemoveEdgeFromTouchingVertices(edge);
                _edges.Remove(edge);

                return true;
            }
            else
            {
                return false;
            }
        }

        public bool RemoveVertices(IEnumerable<V> vertices)
        {
            bool modified = false;

            foreach (var vertex in vertices) 
            {
                modified |= RemoveVertex(vertex);
            }

            return modified;
        }

        public bool RemoveVertex(V vertex)
        {
            if (ContainsVertex(vertex))
            {
                var touchingEdgesList = EdgesOf(vertex);

                // converting to array since iterating over the list will cause 
                // concurrent modification exception
                RemoveEdges(touchingEdgesList.ToArray());
                
                _vertexEdgeMap.Remove(vertex);

                return true;
            }
            else
            {
                return false;
            }
        }

        public V GetEdgeSource(E edge)
        {
            return edge.Source;
        }

        public V GetEdgeTarget(E edge)
        {
            return edge.Target;
        }

        #endregion IGraph

        #region IUndirectedGraph

        public int DegreeOf(V vertex)
        {
            // TODO::consider if we need to throw an exception in case of unknown vertex
            return _vertexEdgeMap[vertex].Count;
        }

        #endregion IUndirectedGraph

        public override string ToString()
        {
            // TODO::implement string representation of the simple graph
            return base.ToString();
        }

        private bool IsAdjucent(
            V sourceVertex,
            V targetVertex,
            E e)
        {
            bool equalStraight =
                sourceVertex.Equals(e.Source)
                && targetVertex.Equals(e.Target);

            // TODO::if equalStraight is true there is no need to continue

            bool equalInverted =
                sourceVertex.Equals(e.Target)
                && targetVertex.Equals(e.Source);

            return equalStraight || equalInverted;
        }

        private bool AssertVertexExist(V vertex)
        {
            if (ContainsVertex(vertex))
            {
                return true;
            }
            else
            {
                throw new ArgumentException(ErrorMessages.VertexNotInGraph);
            }
        }

        private void AddEdgeToTouchingVertices(E edge)
        {
            _vertexEdgeMap[edge.Source].Add(edge);
            _vertexEdgeMap[edge.Target].Add(edge);
        }

        private void RemoveEdgeFromTouchingVertices(E edge)
        {
            _vertexEdgeMap[edge.Source].Remove(edge);
            _vertexEdgeMap[edge.Target].Remove(edge);
        }
    }
}
