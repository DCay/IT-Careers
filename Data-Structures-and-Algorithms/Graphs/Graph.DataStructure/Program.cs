using System;

namespace Graph.DataStructure
{
    class Program
    {
        static void Main(string[] args)
        {
            //Graph<string> graph = new Graph<string>();

            //graph.Add("A");
            //graph.Add("B");
            //graph.Add("C");
            //graph.Add("D");
            //graph.Add("E");
            //graph.Add("F");

            //graph.Connect("A", "B");
            //graph.Connect("A", "C");
            //graph.Connect("B", "D");
            //graph.Connect("B", "E");
            //graph.Connect("C", "F");
            //graph.Connect("D", "C");
            //graph.Connect("D", "F");
            //graph.Connect("E", "D");

            //graph.TopologicalSort().ForEach(Console.WriteLine);

            //Graph<int> graph = new Graph<int>();

            //graph.Add(7);
            //graph.Add(8);
            //graph.Add(2);
            //graph.Add(5);
            //graph.Add(11);
            //graph.Add(9);
            //graph.Add(3);
            //graph.Add(10);

            //graph.Connect(7, 8);
            //graph.Connect(7, 11);
            //graph.Connect(5, 11);
            //graph.Connect(11, 2);
            //graph.Connect(11, 9);
            //graph.Connect(8, 9);
            //graph.Connect(11, 10);
            //graph.Connect(3, 10);
            //graph.Connect(3, 8);

            //graph.TopologicalSort().ForEach(Console.WriteLine);

            Graph<int> graph = new Graph<int>();

            graph.Add(7);
            graph.Add(11);
            graph.Add(8);
            graph.Add(5);

            graph.Connect(7, 11);
            graph.Connect(11, 8);
            graph.Connect(8, 5);
            graph.Connect(5, 11);

            graph.TopologicalSort().ForEach(Console.WriteLine);
        }
    }
}
