using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSPme
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    public class PMBFSolver
    {
        public List<City> Cities { get; set; }
        private double bestDistance = double.MaxValue;
        private List<City> bestRoute = null;
        private object lockObject = new object();  // Used for synchronization

        public PMBFSolver(List<City> cities)
        {
            // Ensure that the city with coordinates (0,0) is always the start city
            Cities = cities.OrderBy(c => c.X == 0 && c.Y == 0 ? 0 : 1).ToList();
        }

        public (List<City> path, double totalDistance) Solve()
        {
            var otherCities = Cities.Skip(1).ToList();
            var startCity = Cities[0];

            // Use parallel processing to handle the initial branching of the permutation tree
            Parallel.ForEach(otherCities, (city) =>
            {
                List<City> currentPath = new List<City>() { startCity, city };
                double currentDistance = startCity.DistanceTo(city);
                var remainingCities = otherCities.Where(c => c != city).ToList();

                Permute(remainingCities, currentPath, currentDistance);
            });

            // Ensure the start city is only at the beginning and end, handled correctly here
            if (bestRoute != null)
            {
                // Calculate the distance to close the loop from the last city back to the start city
                bestDistance += bestRoute.Last().DistanceTo(startCity);
                bestRoute.Add(startCity);  // Complete the cycle by returning to the start city
            }

            return (bestRoute, bestDistance);
        }

        private void Permute(List<City> remainingCities, List<City> currentPath, double currentDistance)
        {
            if (!remainingCities.Any())
            {
                lock (lockObject)  // Ensure thread safety when updating the best results
                {
                    if (currentDistance < bestDistance)
                    {
                        bestDistance = currentDistance;
                        bestRoute = new List<City>(currentPath);
                    }
                }
            }
            else
            {
                foreach (var city in remainingCities)
                {
                    double newDistance = currentDistance + currentPath.Last().DistanceTo(city);

                    if (newDistance >= bestDistance)
                        continue;

                    currentPath.Add(city);
                    var newRemaining = remainingCities.Where(c => c != city).ToList();

                    Permute(newRemaining, currentPath, newDistance);
                    currentPath.RemoveAt(currentPath.Count - 1);
                }
            }
        }
    }
}
