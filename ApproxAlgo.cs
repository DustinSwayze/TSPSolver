using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSPme
{
    public class ApproxAlgo
    {
        private List<City> Cities { get; set; }
        private Dictionary<City, List<Tuple<City, double>>> MST = new Dictionary<City, List<Tuple<City, double>>>();

        public ApproxAlgo(List<City> cities)
        {
            Cities = cities;
            foreach (City city in cities)
            {
                foreach (City other in cities)
                {
                    if (city != other)
                    {
                        city.AddEdge(other);
                    }
                }
            }
        }

        public List<City> Solve()
        {
            City root = Cities[0];  // Choose the first city as root for simplicity
            GenerateMSTPrim(root);
            List<City> preorderPath = PreorderWalk(root);
            return CreateHamiltonianCycle(preorderPath);
        }

        private void GenerateMSTPrim(City root)
        {
            var edgeComparer = Comparer<Tuple<City, double>>.Create((x, y) => x.Item2.CompareTo(y.Item2));
            var priorityQueue = new PriorityQueue<City, double>();
            var inMST = new HashSet<City>();

            priorityQueue.Enqueue(root, 0);

            while (priorityQueue.Count > 0)
            {
                var current = priorityQueue.Dequeue();
                inMST.Add(current);

                foreach (var edge in current.Edges)
                {
                    City adj = edge.Item1;
                    double weight = edge.Item2;
                    if (!inMST.Contains(adj) && !priorityQueue.UnorderedItems.Any(x => x.Element == adj && x.Priority <= weight))
                    {
                        priorityQueue.Enqueue(adj, weight);
                        if (MST.ContainsKey(current))
                        {
                            MST[current].Add(new Tuple<City, double>(adj, weight));
                        }
                        else
                        {
                            MST[current] = new List<Tuple<City, double>>() { new Tuple<City, double>(adj, weight) };
                        }
                    }
                }
            }
        }

        private List<City> PreorderWalk(City root)
        {
            List<City> order = new List<City>();
            PreorderHelper(root, new HashSet<City>(), order);
            return order;
        }

        private void PreorderHelper(City node, HashSet<City> visited, List<City> order)
        {
            visited.Add(node);
            order.Add(node);
            if (MST.ContainsKey(node))
            {
                foreach (var adj in MST[node])
                {
                    if (!visited.Contains(adj.Item1))
                    {
                        PreorderHelper(adj.Item1, visited, order);
                    }
                }
            }
        }

        private List<City> CreateHamiltonianCycle(List<City> preorderPath)
        {
            HashSet<City> visited = new HashSet<City>();
            List<City> cycle = new List<City>();

            foreach (City city in preorderPath)
            {
                if (!visited.Contains(city))
                {
                    visited.Add(city);
                    cycle.Add(city);
                }
            }
            cycle.Add(cycle[0]); // Closing the loop to form a cycle
            return cycle;
        }
    }
}
