using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSPme
{
    public class NaiveBruteForceSolver
    {
        private List<City> Cities { get; set; }

        public NaiveBruteForceSolver(List<City> cities)
        {
            Cities = new List<City>(cities);
        }

        public (List<City> path, double totalDistance) Solve()
        {
            List<City> bestPath = null;
            double minDistance = double.MaxValue;

            // Generate all permutations of the cities, skipping the first city as the starting point
            foreach (var perm in Permute(Cities.Skip(1).ToList()))
            {
                // Add the starting city at the beginning and end to form a loop
                var path = new List<City> { Cities[0] };
                path.AddRange(perm);
                path.Add(Cities[0]);

                // Calculate the distance of the current path
                double currentDistance = CalculateTotalDistance(path);
                if (currentDistance < minDistance)
                {
                    minDistance = currentDistance;
                    bestPath = path;
                }
            }

            return (bestPath, minDistance);
        }

        private IEnumerable<List<City>> Permute(List<City> cities)
        {
            if (!cities.Any())
            {
                yield return new List<City>();
            }
            else
            {
                for (int i = 0; i < cities.Count; i++)
                {
                    City current = cities[i];
                    var others = cities.Take(i).Concat(cities.Skip(i + 1)).ToList();

                    foreach (var perm in Permute(others))
                    {
                        yield return new List<City> { current }.Concat(perm).ToList();
                    }
                }
            }
        }

        private double CalculateTotalDistance(List<City> path)
        {
            double totalDistance = 0;
            for (int i = 0; i < path.Count - 1; i++)
            {
                totalDistance += path[i].DistanceTo(path[i + 1]);
            }
            return totalDistance;
        }
    }
}
