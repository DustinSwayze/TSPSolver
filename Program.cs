using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace TSPme
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            List<City> cities = new List<City>
            {
                new City(0, 0, 0),
                new City(1, -3138, -2512),
                new City(2, 6804, -1072),
                new City(3, -193, 8782),
                new City(4, -5168, 2636), 
                new City(5, -8022, -3864),
                new City(6, -9955, -2923),
                new City(7, -7005, 2118),
                new City(8, 7775, -8002),
                new City(9, 4244, -1339),
                new City(10, 9478, -1973),
                new City(11, -7795, -5000),
                new City(12, -4521, 1266),
                new City(13, -192, 3337),
                new City(14, -9860, 1311)
            };


            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("\nChoose a TSP Solver:");
                Console.WriteLine("1. Nearest Neighbor Heuristic");
                Console.WriteLine("2. Approximation Algorithm");
                Console.WriteLine("3. Naive Brute Force Solver (DO NOT CHOOSE THIS)");
                Console.WriteLine("4. Modified Brute Force");
                Console.WriteLine("5. Exit");
                Console.Write("Enter your choice (1-5): ");

                string input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        NN(cities);
                        break;
                    case "2":
                        AA(cities);
                        break;
                    case "3":
                        NBF(cities);
                        break;
                    case "4":
                        MBF(cities);
                        break;
                    case "5":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid choice, please choose a number between 1 and 5.");
                        break;
                }
            }
        }

        private static double CalculateTotalDistance(List<City> path)
        {
            double totalDistance = 0;
            for (int i = 0; i < path.Count - 1; i++)
            {
                totalDistance += path[i].DistanceTo(path[i + 1]);
            }
            return totalDistance;
        }

        public static void AA(List<City> cities)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            ApproxAlgo AAsolver = new ApproxAlgo(cities);
            List<City> resultPath = AAsolver.Solve();

            double totalDistance = CalculateTotalDistance(resultPath);

            stopwatch.Stop();
            
            // Generate path output
            string pathOutput = "City Path: " + string.Join(" -> ", resultPath.Select(city => city.Id));
            Console.WriteLine(pathOutput);
            Console.WriteLine($"Total distance traveled: {totalDistance:N2} units");
            Console.WriteLine($"Time taken: {stopwatch.ElapsedMilliseconds} ms");

        }
        public static void NN(List<City> cities)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            NearestNeighborSolver nnSolver = new NearestNeighborSolver(cities);
            stopwatch.Stop();
            
            DisplayResults(nnSolver.Solve());
            Console.WriteLine($"Time taken: {stopwatch.ElapsedMilliseconds} ms");

        }

        public static void NBF(List<City> cities)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            NaiveBruteForceSolver nbfSolver = new NaiveBruteForceSolver(cities);
            var (resultPath, totalDistance) = nbfSolver.Solve();

            

            

            DisplayResults(nbfSolver.Solve());
            stopwatch.Stop();
            Console.WriteLine($"Time taken: {stopwatch.ElapsedMilliseconds} ms");

            // Output the results
            //string pathOutput = "City Path: " + string.Join(" -> ", resultPath.Select(city => city.Id));
            //Console.WriteLine(pathOutput);
            //Console.WriteLine($"Total distance traveled: {totalDistance:N2} units");
        }

        public static void MBF(List<City> cities)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            ModifiedBruteForceSolver mbfSolver = new ModifiedBruteForceSolver(cities);

            
            
            DisplayResults(mbfSolver.Solve());
            stopwatch.Stop();
            Console.WriteLine($"Time taken: {stopwatch.ElapsedMilliseconds} ms");
        }


        private static void DisplayResults((List<City> path, double totalDistance) result)
        {
            var (path, totalDistance) = result;
            string pathOutput = "City Path: " + string.Join(" -> ", path.Select(city => city.Id));
            Console.WriteLine(pathOutput);
            Console.WriteLine($"Total distance traveled: {totalDistance:N2} units");
        }

            /*



            /////////// NearestNeighbor Solver

            /*NearestNeighborSolver NNSolver = new NearestNeighborSolver(cities);

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            var (resultPath, totalDistance) = NNSolver.Solve();
            stopwatch.Stop();

            Console.WriteLine($"Time taken: {stopwatch.ElapsedMilliseconds} ms");
            Console.WriteLine("Visited order:");
            foreach (City city in resultPath)
            {
                Console.WriteLine($"City {city.Id} at ({city.X}, {city.Y})");
            }

            Console.WriteLine($"Total distance traveled: {totalDistance:N2} units");
            string pathOutput = "City Path : " + string.Join(" -> ", resultPath.Select(city => city.Id));
            Console.WriteLine(pathOutput);*/

            ///////////////////  Start approxalgo ///////////////////////
            

            /*ApproxAlgo AAsolver = new ApproxAlgo(cities);
            List<City> resultPath = AAsolver.Solve();

            double totalDistance = CalculateTotalDistance(resultPath);

            // Generate path output
            string pathOutput = "City Path: " + string.Join(" -> ", resultPath.Select(city => city.Id));
            Console.WriteLine(pathOutput);
            Console.WriteLine($"Total distance traveled: {totalDistance:N2} units");*/
        
        
    }
}