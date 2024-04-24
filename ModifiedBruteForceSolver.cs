using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSPme
{
    public class MBFSolver
    {
        public List<City> Cities { get; set; }
        private double bestDistance = double.MaxValue;
        private List<City> bestRoute = null;

        public MBFSolver(List<City> cities)
        {
            // Ensure that the city with coordinates (0, 0) is always the start city
            Cities = cities.OrderBy(c => c.X == 0 && c.Y == 0 ? 0 : 1).ToList();
        }

        public (List<City> path, double totalDistance) Solve()
        {
            List<City> startCity = new List<City> { Cities[0] };
            var otherCities = Cities.Skip(1).ToList();

            // Start permutations with an empty path and 0 initial distance
            Permute(otherCities, new List<City>(), 0);

            // Ensure the start and end city is (0, 0)
            if (bestRoute != null)
            {
                bestRoute.Insert(0, Cities[0]);
                bestRoute.Add(Cities[0]);
                bestDistance += Cities[0].DistanceTo(bestRoute[1]) + bestRoute[bestRoute.Count - 2].DistanceTo(Cities[0]);
            }

            return (bestRoute, bestDistance);
        }

        private void Permute(List<City> remainingCities, List<City> currentPath, double currentDistance)
        {
            if (!remainingCities.Any())
            {
                if (currentDistance < bestDistance)
                {
                    bestDistance = currentDistance;
                    bestRoute = new List<City>(currentPath);
                }
            }
            else
            {
                foreach (var city in remainingCities)
                {
                    double newDistance = currentPath.Any() ? currentDistance + currentPath.Last().DistanceTo(city) : 0;

                    if (newDistance >= bestDistance)
                        continue;  // Prune this path as it's already worse than the best found

                    currentPath.Add(city);
                    var newRemaining = remainingCities.Where(c => c != city).ToList();

                    Permute(newRemaining, currentPath, newDistance);
                    currentPath.RemoveAt(currentPath.Count - 1);
                }
            }
        }
    }
}
