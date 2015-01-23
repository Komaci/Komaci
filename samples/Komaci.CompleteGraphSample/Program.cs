using Komaci.Core;
using Komaci.Core.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Komaci.CompleteGraphSample
{
    class Program
    {
        static void Main(string[] args)
        {
            var stringGraph = CreateStringGraph();

            Console.WriteLine("Graph has {0} vertices and {1} edges.", 
                stringGraph.Vertices.Count(), stringGraph.Edges.Count());
        }

        private static IUndirectedGraph<String, Edge<String>> CreateStringGraph()
        {
            var g = new SimpleGraph<String, Edge<String>>(new EdgeFactory<string, Edge<string>>());

            var vertex1 = "1";
            var vertex2 = "2";
            var vertex3 = "3";
            var vertex4 = "4";

            // add the vertices
            g.AddVertex(vertex1);
            g.AddVertex(vertex2);
            g.AddVertex(vertex3);
            g.AddVertex(vertex4);

            // add edges to create a circuit
            g.AddEdge(vertex1, vertex2);
            g.AddEdge(vertex2, vertex3);
            g.AddEdge(vertex3, vertex4);
            g.AddEdge(vertex4, vertex1);

            return g;
        }
    }
}
