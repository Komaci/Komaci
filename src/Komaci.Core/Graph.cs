//The MIT License (MIT)
//
//Copyright (c) 2015 Komaci, Avo M.
//
//Permission is hereby granted, free of charge, to any person obtaining a copy
//of this software and associated documentation files (the "Software"), to deal
//in the Software without restriction, including without limitation the rights
//to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//copies of the Software, and to permit persons to whom the Software is
//furnished to do so, subject to the following conditions:
//
//The above copyright notice and this permission notice shall be included in all
//copies or substantial portions of the Software.
//
//THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
//SOFTWARE.

using System;
using System.Collections.Generic;
using System.Linq;

namespace Komaci.Core
{
    public static class Graph<V, E>
        where E : IEdge<V>
    {
        public static E AddEdge(
            IGraph<V, E> graph,
            V sourceVertex,
            V targetVertex)
        {
            var edge = graph.EdgeFactory.CreateEdge(sourceVertex, targetVertex);
            return graph.AddEdge(sourceVertex, targetVertex, edge) ? edge : default(E);
        }

        public static E AddEdge(
            IWeightedGraph<V, E> graph,
            V sourceVertex,
            V targetVertex,
            double weight)
        {
            var edge = graph.EdgeFactory.CreateEdge(sourceVertex, targetVertex);
            graph.SetEdgeWeight(edge, weight);
            return graph.AddEdge(sourceVertex, targetVertex, edge) ? edge : default(E);
        }

        public static E AddEdgeWithVertices(
            IGraph<V, E> graph,
            V sourceVertex,
            V targetVertex)
        {
            graph.AddVertex(sourceVertex);
            graph.AddVertex(targetVertex);

            return graph.AddEdge(sourceVertex, targetVertex);
        }

        public static bool AddEdgeWithVertices(
            IGraph<V, E> targetGraph,
            IGraph<V, E> sourceGraph,
            E edge)
        {
            V sourceVertex = edge.Source;
            V targetVertex = edge.Target;

            targetGraph.AddVertex(sourceVertex);
            targetGraph.AddVertex(targetVertex);

            return targetGraph.AddEdge(sourceVertex, targetVertex, edge);
        }

        public static E AddEdgeWithVertices(
            IWeightedGraph<V, E> graph,
            V sourceVertex,
            V targetVertex,
            double weight)
        {
            graph.AddVertex(sourceVertex);
            graph.AddVertex(targetVertex);

            return AddEdge(graph, sourceVertex, targetVertex, weight);
        }

        public static bool AddGraph(
            IGraph<V, E> destination,
            IGraph<V, E> source)
        {
            bool modified = AddAllVertices(destination, source.Vertices);
            modified |= AddAllEdges(destination, source, source.Edges);

            return modified;
        }

        public static void AddGraphReversed(
            IDirectedGraph<V, E> destination,
            IDirectedGraph<V, E> source)
        {
            AddAllVertices(destination, source.Vertices);

            foreach (var edge in source.Edges)
            {
                destination.AddEdge(edge.Target, edge.Source);
            }
        }

        public static bool AddAllEdges(
            IGraph<V, E> destination,
            IGraph<V, E> source,
            IEnumerable<E> edges)
        {
            bool modified = false;

            foreach (var edge in edges)
            {
                V sourceVertex = edge.Source;
                V targetVertex = edge.Target;
                destination.AddVertex(sourceVertex);
                destination.AddVertex(targetVertex);
                modified |= destination.AddEdge(sourceVertex, targetVertex, edge);
            }

            return modified;
        }

        public static bool AddAllVertices(
            IGraph<V, E> destination,
            IEnumerable<V> vertices)
        {
            bool modified = false;

            foreach (var vertex in vertices)
            {
                modified |= destination.AddVertex(vertex);
            }

            return modified;
        }

        public static IEnumerable<V> GetVertexNeighbors(IGraph<V, E> graph, V vertex)
        {
            var neighbors = new List<V>();

            foreach (var edge in graph.EdgesOf(vertex))
            {
                neighbors.Add(GetOppositeVertex(graph, edge, vertex));
            }

            return neighbors;
        }

        public static IEnumerable<V> GetVertexPredecessors(
            IDirectedGraph<V, E> graph,
            V vertex)
        {
            var predecessors = new List<V>();
            var edges = graph.IncomingEdgesOf(vertex);

            foreach (var edge in edges)
            {
                predecessors.Add(GetOppositeVertex(graph, edge, vertex));
            }

            return predecessors;
        }

        public static IEnumerable<V> GetVertexSuccessors(
            IDirectedGraph<V, E> graph,
            V vertex)
        {
            var successors = new List<V>();
            var edges = graph.OutgoingEdgesOf(vertex);

            foreach (var edge in edges)
            {
                successors.Add(GetOppositeVertex(graph, edge, vertex));
            }

            return successors;
        }

        public static bool IsIncident(IGraph<V, E> graph, E edge, V vertex)
        {
            return (edge.Source.Equals(vertex))
                || (edge.Target.Equals(vertex));
        }

        public static V GetOppositeVertex(IGraph<V, E> graph, E edge, V vertex)
        {
            V source = edge.Source;
            V target = edge.Target;

            if (vertex.Equals(source))
            {
                return target;
            }
            else if (vertex.Equals(target))
            {
                return source;
            }
            else
            {
                throw new ArgumentException("no such vertex");
            }
        }

        public static IEnumerable<V> GetPathVertexList(IGraphPath<V, E> path)
        {
            IGraph<V, E> graph = path.Graph;
            var pathVerices = new List<V>();
            var vertex = path.StartVertex;
            pathVerices.Add(vertex);

            foreach (var edge in path.Edges)
            {
                vertex = GetOppositeVertex(graph, edge, vertex);
                pathVerices.Add(vertex);
            }

            return pathVerices;
        }

        // TODO::Add directed to undirected graph converter here 
    }
}
