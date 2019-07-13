using System.IO;

namespace Q123_14F8028
{
    class Map
    {
        public static double intervalX;
        public static double intervalY;
        public static City[] Cities { get; set; }
        public static void PickMap(string filename, int Size)
        {
            Cities = new City[Size];
            using (TextReader reader = File.OpenText(filename))
            {
                string text;
                for (int i = 0; i < Size; i++)
                {
                    text = reader.ReadLine();
                    string[] cordinates = text.Split(' ');
                    double x = double.Parse(cordinates[0]);
                    double y = double.Parse(cordinates[1]);
                    Cities[i] = new City(x, y, i);
                }
            }
            intervalX = maxX() - minX() / 5;
            intervalY = maxY() - minY() / 5;
        }
        public static double minX()
        {
            double x = Cities[0].x;
            for (int i = 1; i < GA.numCities; i++)
            {
                if (Cities[i].x < x)
                {
                    x = Cities[i].x;
                }
            }
            return x;
        }
        public static double minY()
        {
            double y = Cities[0].y;
            for (int i = 1; i < GA.numCities; i++)
            {
                if (Cities[i].y < y)
                {
                    y = Cities[i].y;
                }
            }
            return y;
        }
        public static double maxX()
        {
            double x = Cities[0].x;
            for (int i = 1; i < GA.numCities; i++)
            {
                if (Cities[i].x > x)
                {
                    x = Cities[i].x;
                }
            }
            return x;
        }
        public static double maxY()
        {
            double y = Cities[0].y;
            for (int i = 1; i < GA.numCities; i++)
            {
                if (Cities[i].y > y)
                {
                    y = Cities[i].y;
                }
            }
            return y;
        }
    }
}
