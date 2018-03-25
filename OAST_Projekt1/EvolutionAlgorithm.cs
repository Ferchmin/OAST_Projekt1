﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAST_Projekt1
{
    class EvolutionAlgorithm
    {
        double mutationProbability { get; set; }
        double crossoverProbability { get; set; }
        int populationNumber { get; set; }
        int seed { get; set; }
        int stopCrit { get; set; }
        int stopParameter { get; set;}
        private int mutationCounter = 0;
        private int generationsCounter = 0;
        private int ammountOfGenerationsWithoutProgress = 0;
        private int bestSolutionPreviousGeneration;
        private int bestSolutionNextGeneration;
        long algorithmTime { get; set; }
 
        Solution solution;
        List<Link> Links = new List<Link>();
        List<Demand> Demands = new List<Demand>();
        List<Solution> Population;

        Random rand;

        public EvolutionAlgorithm(List<Link> Links, List<Demand> Demands)
        {
            LoadAlgorithmParameters();         
            InitiateAlgorithm(Demands);
        }
        //Metoda odpowiedzialna za wczytanie parametrow algorytmu
        void LoadAlgorithmParameters()
        {
            Console.WriteLine("Type parameters for your simulation:");
            #region LoadPopulation
            Console.WriteLine("Type number of population:");
            try
            {
                populationNumber = Int32.Parse(Console.ReadLine());
            }
            catch
            {
                Console.WriteLine("Wrong input was provided. Please try again:");
                LoadAlgorithmParameters();
            }
            #endregion
            #region LoadMutation
            Console.WriteLine("Type probability of mutation for example 0,01:");
            try
            {
                crossoverProbability = Double.Parse(Console.ReadLine());
                if (crossoverProbability > 1)
                    crossoverProbability = 1;
            }
            catch
            {
                Console.WriteLine("Wrong input was provided. Please try again:");
                LoadAlgorithmParameters();
            }
            #endregion
            #region LoadCrossover
            Console.WriteLine("Type probability of crossover for example 0,01:");
            try
            {
                mutationProbability = Double.Parse(Console.ReadLine());
            }
            catch
            {
                Console.WriteLine("Wrong input was provided. Please try again:");
                LoadAlgorithmParameters();
            }
            #endregion
            #region LoadStopCriterium
            Console.WriteLine("Type criterium of stop for algorithm:\n{0}\n{1}\n{2}\n{3}",
                "1 - time",
                "2 - number of generations",
                "3 - number of mutations",
                "4 - no progress in number of solutions");
            try
            {
                stopCrit = Int32.Parse(Console.ReadLine());
                switch (stopCrit)
                {
                    case 1:
                        Console.WriteLine("Type time in seconds:");
                        stopParameter= Int32.Parse(Console.ReadLine());
                        break;
                    case 2:
                        Console.WriteLine("Type number of generations");
                        stopParameter = Int32.Parse(Console.ReadLine());
                        break;
                    case 3:
                        Console.WriteLine("Type number of mutations");
                        stopParameter = Int32.Parse(Console.ReadLine());
                        break;
                    case 4:
                        Console.WriteLine("Type ammount of generations without progress");
                        stopParameter = Int32.Parse(Console.ReadLine());
                        break;
                }
            }
            catch
            {
                Console.WriteLine("Wrong input was provided. Please try again:");
                LoadAlgorithmParameters();
            }
            #endregion
            #region LoadSeed
            Console.WriteLine("Type seed for random number generator:");
            try
            {
                seed = Int32.Parse(Console.ReadLine());
                rand = new Random(seed);
            }
            catch
            {
                Console.WriteLine("Wrong input was provided. Please try again:");
                LoadAlgorithmParameters();
            }
            #endregion
        }
        //Metoda w której znajduje się główna pętla programu
        void InitiateAlgorithm(List<Demand> Demands)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            //1. Stworzenie populacji
            Population = CreatePopulation(Demands, populationNumber);
            //2. Obliczenie funkcji celu dla kazdego osobnika z populacji
            ObjectiveFunctionForPopulation(Population);
            Population = Population.OrderByDescending(x => x.objectiveFunctionResult).ToList();

            while (true)
            {
                bestSolutionPreviousGeneration = Population[0].objectiveFunctionResult;

                //3. Krzyzówki osobników
                for (int i = 0; i < populationNumber / 2; i += 2)
                {
                    Population.Add(Crossover(Population[i], Population[i + 1]));
                }
                //4. Mutacje osobników
                for (int i = 0; i < populationNumber; i++)
                {
                    Population.Add(Mutation(Population[i]));
                }
                //5. Usuwanie najsłabszych osobników
                Population = Population.OrderByDescending(x => x.objectiveFunctionResult).ToList();
                Population.RemoveRange(populationNumber, Population.Count - populationNumber);
                bestSolutionNextGeneration = Population[0].objectiveFunctionResult;

                //Sprawdzenie czasu dzialania algorytmu ewolucyjnegoo

              
                stopwatch.Stop();
                double time = stopwatch.ElapsedMilliseconds / 1000;
                stopwatch.Start();
                algorithmTime = (int)(Math.Ceiling(time));

                //Nastawianie licznika generacji
                generationsCounter++;

                if (bestSolutionPreviousGeneration > bestSolutionNextGeneration)
                    ammountOfGenerationsWithoutProgress++;
               
                //Sprawdzenie warunku kryterium stopu
                if (validateStopCriterium(stopCrit))
                    break;
            }
        }
        //Metoda odpowiedzialna za losowe wygenerowanie populacji początkowej
        public List<Solution> CreatePopulation(List<Demand> Demands, int populationNumber)
        {
            //Ustalenie liczby osobników w populacji
            Population = new List<Solution>();
            for (int i = 0; i < populationNumber; i++)
            {
                List<Demand> localDemands = new List<Demand>();
                foreach (Demand demand in Demands)
                {
                    localDemands.Add(demand);
                }
                Population.Add(solution = new Solution(localDemands));
            }

            //Losowanie parametrów dla populacji        
            foreach (Solution solution in Population)
            {
                foreach (Demand demand in solution.Demands)
                {
                    demand.UsedPaths.Clear();
                    for (int l = 1; l <= demand.AvailablePaths.Count; l++)
                    {
                        demand.UsedPaths.Add(0);
                    }
                    for (int m = 0; m < demand.demandedCapacity; m++)
                    {
                        int randomIndex = rand.Next(demand.AvailablePaths.Count());
                        demand.UsedPaths[randomIndex] = demand.UsedPaths[randomIndex] + 1;

                    }
                }

            }
            Crossover(Population[0], Population[1]);
            Mutation(Population[0]);
            return Population;
        }
        //Metoda odpowiadająca za krzyżowanie 
        Solution Crossover(Solution parent1, Solution parent2)
        {
            Solution child = new Solution();

            if (rand.NextDouble() < crossoverProbability)
            {
                for (int i = 0; i < parent1.Demands.Count; i++)
                {
                    if (rand.Next(0, 2) < 1)
                    {
                        child.Demands.Add(parent1.Demands[i]);
                    }
                    else
                        child.Demands.Add(parent2.Demands[i]);
                }
            }
            return child;
        }
        //Metoda odpowiadająca za mutację
        Solution Mutation(Solution solutionForMute)
        {
            Solution mutadedSolution = new Solution(solutionForMute);

            foreach (Demand demand1 in solutionForMute.Demands)
            {
                if (rand.NextDouble() < mutationProbability)
                {
                    DistributeLambdas(demand1);
                }
            }
            return solutionForMute;
        }
        //Metoda odpowiadająca za rożdział lambd przy mutacji
        void DistributeLambdas(Demand demand1)
        {
            int randomIndex = rand.Next(0, demand1.UsedPaths.Count);

            if (demand1.UsedPaths[randomIndex] == 0)
            {
                DistributeLambdas(demand1);
                mutationCounter++;
            }
            else
                demand1.UsedPaths[randomIndex] = demand1.UsedPaths[randomIndex] - 1;
            randomIndex = rand.Next(0, demand1.UsedPaths.Count);
            demand1.UsedPaths[randomIndex] = demand1.UsedPaths[randomIndex] + 1;
        }
        //Metoda uzywana do obliczania funkcji celu dla całej populacji
        void ObjectiveFunctionForPopulation(List<Solution> Population)
        {
            foreach (Solution solution in Population)
            {
                List<int> linkResults = new List<int>();
                foreach (Demand demand in solution.Demands)
                {
                    foreach (Path path in demand.AvailablePaths)
                    {
                        foreach (Link link in path.Links)
                        {
                            linkResults.Add(Math.Max(0, link.usedCapacity - link.capacity));
                        }
                    }

                }
                solution.objectiveFunctionResult = Math.Max(0, linkResults.Max());
            }

        }

        bool validateStopCriterium(int stopCriterium)
        {
            bool isStop = false;
            switch (stopCriterium)
            {
                case 1:
                    if (stopParameter < algorithmTime)
                        isStop = true;
                    break;
                case 2:
                    if(stopParameter < generationsCounter)
                        isStop = true;
                    break;
                case 3:
                    if (stopParameter < mutationCounter)
                        isStop = true;
                    break;
                case 4:
                    if (stopParameter < ammountOfGenerationsWithoutProgress)
                        isStop = true;
                    break;
            }
            return isStop;
        }

    }
}


