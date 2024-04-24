using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSPme
{
    public class City
    {
        public int Id { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public List<Tuple<City, double>> Edges { get; private set; } = new List<Tuple<City, double>>();

        public City(int id, int x, int y)
        {
            Id = id;
            X = x;
            Y = y;
        }

        // Calculate the Euclidean distance to another city
        public double DistanceTo(City other)
        {
            return Math.Sqrt(Math.Pow(this.X - other.X, 2) + Math.Pow(this.Y - other.Y, 2));
        }

        public void AddEdge(City toCity)
        {
            Edges.Add(new Tuple<City, double>(toCity, DistanceTo(toCity)));
        }
    }


}
