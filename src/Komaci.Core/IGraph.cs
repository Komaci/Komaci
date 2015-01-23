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

namespace Komaci.Core
{
    /// <summary>
    /// The root interface for all graph implementations.
    /// Defines methods to manipulate generic graph.
    /// </summary>
    /// <typeparam name="V">The type of vertex in the graph.</typeparam>
    /// <typeparam name="E">The type of edge in the graph</typeparam>
    public interface IGraph<V, E>
        where E : IEdge<V>
    {
        bool LoopsAllowed { get; }

        bool MultipleEdgesAllowed { get; }

        /// <summary>
        /// Gets the edge factory using which the graph creates new edges.
        /// </summary>
        IEdgeFactory<V, E> EdgeFactory { get; }

        IEnumerable<E> Edges { get; }

        IEnumerable<V> Vertices { get; }

        /// <summary>
        /// Gets an edge connecting source vertex to target vertex if such 
        /// vertices and such edge exist in the graph. 
        /// </summary>
        /// <param name="sourceVertex">Source vertex of the edge.</param>
        /// <param name="targetVertex">Target vertex of the edge.</param>
        /// <returns>An edge connecting source vertex to target vertex.</returns>
        /// <remarks>
        /// In undirected graphs, the returned edge may have its source and target vertices in the opposite order.
        /// </remarks>
        E GetEdge(V sourceVertex, V targetVertex);

        // TODO::consider returning bool to indicate whether edge was added or not
        // and have another method that will add and return added edge
        E AddEdge(V sourceVertex, V targetVertex);

        bool AddEdge(V sourceVertex, V targetVertex, E edge);

        bool AddVertex(V vertex);

        bool ContainsEdge(V sourceVertex, V targetVertex);

        bool ContainsEdge(E edge);

        bool ContainsVertex(V vertex);

        IEnumerable<E> EdgesOf(V vertex);

        // TODO::consider adding this
        //IEnumerable<E> IncidentEdgesOf(V vertex);

        // TODO::consider adding this
        //IEnumerable<V> AdjacentVerticesOf(V vertex);

        // TODO::consider returning int which will be the count of vertices deleted
        //      or may be list with the removed vertices
        bool RemoveEdges(IEnumerable<E> edges);

        E RemoveEdge(V sourceVertex, V targetVertex);

        bool RemoveEdge(E edge);

        // TODO::consider returning int which will be the count of vertices deleted
        //      or may be list with the removed vertices
        bool RemoveVertices(IEnumerable<V> vertices);

        bool RemoveVertex(V vertex);

        V GetEdgeSource(E edge);

        V GetEdgeTarget(E edge);

        // TODO::consider adding this
        //IUndirectedGraph<V, E> ToUndirectedGraph();
    }
}
