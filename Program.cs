namespace Q123_14F8028
{
    class Program
    {
        static void Main(string[] args)
        {

            Map.PickMap(GA.fileName, GA.numCities);
            Population pop = new Population(GA.popSize, true);

            System.Console.WriteLine("Mutation Probability: " + GA.mutRate);
            System.Console.WriteLine("Iterations: " + GA.iterations);
            System.Console.WriteLine("Population Size: " + GA.popSize);
            System.Console.WriteLine("Initial distance: " + pop.getFittest().getDistance());

            pop = GA.evolvePopulation(pop);
            for (int i = 0; i < GA.iterations; i++)
            {
                pop = GA.evolvePopulation(pop);
            }

            System.Console.WriteLine("Finished");
            System.Console.WriteLine("Final distance: " + pop.getFittest().getDistance());
            System.Console.WriteLine("Solution:");
            System.Console.WriteLine(pop.getFittest());
            System.Console.ReadKey();
        }
    }
}
