using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSPme
{
    public class NearestNeighborSolver
    {
        private List<City> Cities { get; set; }

        public NearestNeighborSolver(List<City> cities)
        {
            Cities = new List<City>(cities);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public (List<City> path, double totalDistance) Solve()
        {
            List<City> path = new List<City>();
            List<City> remainingCities = new List<City>(Cities);

            double totalDistance = 0;

            City current = remainingCities[0];
            path.Add(current);
            remainingCities.Remove(current);


            while (remainingCities.Count > 0)
            {
                remainingCities.Sort((city1, city2) => current.DistanceTo(city1).CompareTo(current.DistanceTo(city2)));

                City nextCity = remainingCities[0];
                totalDistance += current.DistanceTo(nextCity);
                current = nextCity;
                path.Add(current);
                remainingCities.Remove(current);
            }

            totalDistance += current.DistanceTo(path[0]);

            return (path, totalDistance);
        }
    }


}
