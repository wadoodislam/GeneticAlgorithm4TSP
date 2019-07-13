using System;

namespace Q123_14F8028
{
    class GA
    {
        //Specifier: 1)SWAP 2)SCRAMBLE 3)INVERSION
        public static string mutType = "INVERSION";
        //Specifier: 1)LINEARRANK 2)FPS 3)TOURNAMENT
        public static string selection = "FPS";
        //Specifier: 1)PMX 2)ORDER1 3)CYCLE
        public static string Xover = "CYCLE";
        //Specifier: 1)true <for shortest distance> 2)false <for longest distance>
        public static bool reverse =false;
        //Specifier: DOMAIN:<0 to popSize>
        public static int pool = 5;
        //Specifier: <NUMBER OF EVOLUTION>
        public static int iterations = 20000;
        //Specifier: DOMAIN:<0.0 to 1.0>     Mutation Probability
        public static double mutRate = 0.03;
        //Specifier: 1)true <ENABLES ELITIZSM> 2) false <DISABLES ELITIZSM>
        public static bool elitism = true;
        //Specifier: DOMAIN:<1 to MAX_INT>
        public static int popSize = 40;
        //Specifier: CONSTANT <used for linear-ranking>
        public static double s = 1.5;
        //Specifier: CONSTANT <NUMBER OF CITIES>
        public static int numCities = 50;
        //Specifier: <NAME OF DATAFILE>
        public static string fileName = "TSP2.txt";
        private static Random r = new Random();

        public static Population evolvePopulation(Population pop)
        {
            Population newPopulation = new Population(popSize, false);
            int elitismOffset = 0;
            if (elitism)
            {
                newPopulation.tours[0] = pop.getFittest();
                elitismOffset = 1;
            }
            for (int i = elitismOffset; i < popSize; i++)
            {
                Tour parent1 = null;
                Tour parent2 = null;
                if (selection == "FPS")
                {
                    parent1 = pop.FPSSelect();
                    parent2 = pop.FPSSelect();
                }
                else if (selection == "TOURNAMENT")
                {
                    parent1 = pop.tournamentSelect();
                    parent2 = pop.tournamentSelect();
                }
                else if (selection == "LINEARRANK")
                {
                    parent1 = pop.linearRankSelect();
                    parent2 = pop.linearRankSelect();
                }
                Tour child = null;
                if (Xover == "ORDER1")
                {
                    child = parent1.ORDER1_Crossover(parent2);
                }
                else if (Xover == "PMX")
                {
                    child = parent1.PMX_Crossover(parent2);
                }
                else if (Xover == "CYCLE")
                {
                    child = parent1.CYCLE_Crossover(parent2);
                }
                newPopulation.tours[i] = child;
            }
            if (r.NextDouble() <= mutRate)
            {
                for (int i = elitismOffset; i < popSize; i++)
                {

                    if (mutType == "SWAP")
                    {
                        newPopulation.tours[i].SwapMutate();
                    }
                    else if (mutType == "SCRAMBLE")
                    {
                        newPopulation.tours[i].ScrambleMutate();
                    }
                    else if (mutType == "INVERSION")
                    {
                        newPopulation.tours[i].InversionMutate();
                    }
                }
            }
            return newPopulation;
        }
    }
}
