using System;

namespace GeneticAlgorithm4TSP
{
    class Population
    {
        public Tour[] tours { get; set; }
        private static Random r = new Random();
        public Population(int popSize, bool initialize)
        {
            tours = new Tour[popSize];
            if (initialize)
            {
                for (int i = 0; i < popSize; i++)
                {
                    Tour newTour = new Tour();
                    newTour.generateIndividual();
                    tours[i] = newTour;
                }
            }
        }
        public Tour getFittest()
        {
            Tour fittest = tours[0];
            for (int i = 1; i < tours.Length; i++)
            {
                if (fittest.getFitness() <= tours[i].getFitness())
                {
                    fittest = tours[i];
                }
            }
            return fittest;
        }
        private double TotalFitness()
        {
            double temp = 0.0;
            for (int i = 0; i < tours.Length; i++)
            {
                temp += tours[i].getFitness();
            }
            return temp;
        }
        private void CalProbabilities()
        {
            double prevProb = 0.0;
            double tF = TotalFitness();
            for (int i = 0; i < tours.Length; i++)
            {
                tours[i].probability = prevProb + (tours[i].getFitness() / tF);
                prevProb = tours[i].probability;
            }
        }
        public Tour FPSSelect()
        {
            CalProbabilities();
            double tmp = r.NextDouble();
            for (int i = 0; i < tours.Length; i++)
            {
                if (tmp < tours[i].probability)
                {
                    return tours[i];
                }
            }
            return null;
        }
        public Tour tournamentSelect()
        {
            Population tournament = new Population(GA.pool, false);
            for (int i = 0; i < GA.pool; i++)
            {
                int randomId = r.Next(tours.Length);
                tournament.tours[i] = tours[randomId];
            }
            Tour fittest = tournament.getFittest();
            return fittest;
        }
        public Tour linearRankSelect()
        {
            Population temppop = new Population(GA.popSize, false);
            for (int i = 0; i < GA.popSize; i++)
            {
                temppop.tours[i] = tours[i];
            }
            double tF = temppop.TotalFitness();
            for (int i = 0; i < GA.popSize; i++)
            {
                temppop.tours[i].probability = (temppop.tours[i].getFitness() / tF);
            }
            Array.Sort(temppop.tours, new Comparison<Tour>((x, y) => (x.probability.CompareTo(y.probability))));

            double prevProb = 0.0;
            for (int i = 0; i < GA.popSize; i++)
            {
                temppop.tours[i].probability = prevProb + ((2 - GA.s) / GA.popSize) + (2 * (i + 1) * (GA.s - 1)) / (GA.popSize * (GA.popSize - 1));
                prevProb = temppop.tours[i].probability;
            }
            Array.Sort(temppop.tours, new Comparison<Tour>((x, y) => (x.probability.CompareTo(y.probability))));
            double tmp = r.NextDouble();
            for (int i = 0; i < GA.popSize; i++)
            {
                if (tmp < temppop.tours[i].probability)
                {
                    return temppop.tours[i];
                }
            }
            return null;
        }
    }
}