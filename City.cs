using System;

namespace GeneticAlgorithm4TSP
{
    class City
    {
        public double x { get; set; }
        public double y { get; set; }
        public int id { get; set; }

        public City(double x, double y, int id)
        {
            this.x = x;
            this.y = y;
            this.id = id;
        }

        public double distance(City city)
        {
            double deltaX = Math.Abs(x - city.x);
            double deltaY = Math.Abs(y - city.y);
            double distance = Math.Sqrt((deltaX * deltaX) + (deltaY * deltaY));
            return distance;
        }
        override
        public string ToString()
        {
            return x + "|" + y;
        }
    }
}
