using System;
using System.Collections.Generic;

namespace Q123_14F8028
{
    class Tour
    {
        private static Random r = new Random();
        public double fitness { get; set; }
        public double probability { get; set; }
        public double distance { get; set; }
        public int[] tour { get; set; }
        public Tour()
        {
            tour = new int[GA.numCities];
            for (int i = 0; i < tour.Length; i++)
            {
                tour[i] = -1;
            }
        }
        public void generateIndividual()
        {
            fitness = 0;
            probability = 0;
            distance = 0;
            for (int i = 0; i < Map.Cities.Length; i++)
            {
                tour[i] = i;
            }
            int n = Map.Cities.Length;
            while (n > 1)
            {
                n--;
                int k = r.Next(n + 1);
                int v = tour[k];
                tour[k] = tour[n];
                tour[n] = v;
            }
        }
        public void SwapMutate()
        {

            int i = r.Next(0, tour.Length);
            int j = r.Next(0, tour.Length);
            int v = tour[i];
            tour[i] = tour[j];
            tour[j] = v;
            fitness = 0;
            probability = 0;
            distance = 0;
        }
        public void InversionMutate()
        {
            int i, j;
            do
            {
                i = r.Next(0, tour.Length);
                j = r.Next(0, tour.Length);
            } while (i == j);
            if (i > j)
            {
                int temp = i;
                i = j;
                j = temp;
            }
            for (int k = i; k < j; k++, j--)
            {
                int temp = tour[i];
                tour[i] = tour[j];
                tour[j] = temp;
            }
            fitness = 0;
            probability = 0;
            distance = 0;
        }
        public void ScrambleMutate()
        {

            int i = r.Next(0, tour.Length);
            int j = r.Next(0, tour.Length);
            do
            {
                i = r.Next(0, tour.Length);
                j = r.Next(0, tour.Length);
            } while (i == j);
            if (i > j)
            {
                int temp = i;
                i = j;
                j = temp;
            }
            int n = j - i + 1;
            while (n > 1)
            {
                n--;
                int k = r.Next(n + 1);
                int v = tour[k + i];
                tour[k + i] = tour[n + i];
                tour[n + i] = v;
            }
            fitness = 0;
            probability = 0;
            distance = 0;
        }
        public bool hasCity(int key)
        {
            for (int i = 0; i < tour.Length; i++)
            {
                if (tour[i] == key)
                {
                    return true;
                }
            }
            return false;
        }
        public double getFitness()
        {
            if (GA.reverse)
            {
                return (double)getDistance();
            }
            if (fitness == 0)
            {
                fitness = 1 / (double)getDistance();
            }
            return fitness;
        }

        public double getDistance()
        {
            if (distance == 0)
            {
                double tourDistance = 0;
                for (int i = 0; i < Map.Cities.Length; i++)
                {

                    City fromCity = Map.Cities[tour[i]];
                    City destinationCity;
                    if (i + 1 < Map.Cities.Length)
                    {
                        destinationCity = Map.Cities[tour[i + 1]];
                    }
                    else
                    {
                        destinationCity = Map.Cities[tour[0]];
                    }
                    tourDistance += fromCity.distance(destinationCity);
                }
                distance = tourDistance;
            }
            return distance;
        }
        public Tour PMX_Crossover(Tour parent2)
        {
            Tour child = new Tour();
            int r1, r2;
            do
            {
                r1 = r.Next(GA.numCities);
                r2 = r.Next(GA.numCities);
            } while (r1 == r2);
            if (r1 > r2)
            {
                int t = r1;
                r1 = r2;
                r2 = t;
            }
            for (int i = r1; i <= r2; i++)
            {
                child.tour[i] = this.tour[i];
            }
            for (int i = r1; i <= r2; i++)
            {
                bool found = false;
                for (int j = 0; j < GA.numCities && !found; j++)
                {
                    found = child.tour[j] == parent2.tour[i];
                }
                if (!found)
                {
                    bool replaced = false;
                    int value = this.tour[i];
                    while (!replaced)
                    {
                        int k = 0;
                        while (parent2.tour[k] != value)
                        {
                            k++;
                        }
                        if (k <= r2 && k >= r1)
                        {
                            value = this.tour[k];
                        }
                        else
                        {
                            child.tour[k] = parent2.tour[i];
                            replaced = true;
                        }
                    }
                }
            }
            for (int i = 0; i < GA.numCities; i++)
            {
                bool found = false;
                for (int j = 0; j < GA.numCities && !found; j++)
                {
                    found = child.tour[j] == parent2.tour[i];
                }
                if (!found)
                {
                    child.tour[i] = parent2.tour[i];
                }
            }
            return child;
        }
        public Tour CYCLE_Crossover(Tour parent2)
        {
            Tour child = new Tour();
            List<List<int>> all = new List<List<int>>();
            for (int i = 0; i < GA.numCities; i++)
            {
                if (!Has(all, i))
                {
                    List<int> cycle = new List<int>();
                    bool cyclefound = false;

                    int Sindex = i;
                    while (!cyclefound)
                    {
                        bool found = false;
                        int j = -1;
                        while (!found)
                        {
                            found = this.tour[++j] == parent2.tour[Sindex];
                        }
                        if (j == i)
                        {
                            cycle.Add(j);
                            cyclefound = true;
                        }
                        else
                        {
                            cycle.Add(j);
                            Sindex = j;
                        }
                    }
                    all.Add(cycle);
                }
            }
            bool flag = false;
            foreach (List<int> i in all)
            {
                if (!flag)
                {
                    foreach (int j in i)
                    {
                        child.tour[j] = this.tour[j];
                    }
                    flag = true;
                }
                else
                {
                    foreach (int j in i)
                    {
                        child.tour[j] = this.tour[j];
                    }
                    flag = false;
                }
            }
            return child;
        }
        public Tour ORDER1_Crossover(Tour parent2)
        {
            Tour child = new Tour();
            int startPos = r.Next(this.tour.Length);
            int endPos = r.Next(this.tour.Length);

            for (int i = 0; i < child.tour.Length; i++)
            {
                if (startPos < endPos && i > startPos && i < endPos)
                {
                    child.tour[i] = this.tour[i];
                }
                else if (startPos > endPos)
                {
                    if (!(i < startPos && i > endPos))
                    {
                        child.tour[i] = this.tour[i];
                    }
                }
            }
            for (int i = 0; i < parent2.tour.Length; i++)
            {
                if (!child.hasCity(parent2.tour[i]))
                {
                    bool flag = false;
                    for (int ii = 0; ii < child.tour.Length && !flag; ii++)
                    {
                        if (child.tour[ii] == -1)
                        {
                            child.tour[ii] = parent2.tour[i];
                            flag = true;
                        }
                    }
                }
            }
            return child;
        }
        private bool Has(List<List<int>> all, int a)
        {
            foreach (List<int> i in all)
            {
                if (i.Contains(a))
                {
                    return true;
                }
            }
            return false;
        }
        override
        public string ToString()
        {
            string s = "";
            for (int i = 0; i < tour.Length; i++)
            {
                s += tour[i] + "|";
            }
            return s;
        }
    }
}
