using System;
using System.Collections.Generic;
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
        enum StopCriterium { time, generationsNumber, mutationsNumber, noProgressSolutionNumber };
        Solution solution;
        List<Link> Links = new List<Link>();
        List<Demand> Demands = new List<Demand>();
        List<Solution> Population;
        

        public EvolutionAlgorithm(List<Link> Links, List<Demand> Demands)
        {
            LoadAlgorithmParameters();
            Population = CreatePopulation(Demands, populationNumber);
            seed = 43453;
        }
        //Generowanie populacji początkowej
        public List<Solution> CreatePopulation(List<Demand> Demands, int populationNumber)
        {
            //Ustalenie liczby osobników w populacji
            Population = new List<Solution>();
            for (int i=0; i < populationNumber; i++)
            {
                Population.Add(solution = new Solution(Demands));                
            }

            //Losowanie parametrów dla populacji        
            foreach (Solution solution in Population)
            {
                
                foreach (Demand demand in Demands)
                {
                    demand.UsedPaths = new List<int>();
                    for (int l = 1; l <= demand.AvailablePaths.Count; l++)
                    {
                        demand.UsedPaths.Add(0);
                    }
                    for (int m = 0; m < demand.demandedCapacity; m++)
                    {
                        //Random rand = new Random(seed);
                        Random rand = new Random(3);
                        int randomIndex = rand.Next(demand.AvailablePaths.Count());
                        demand.UsedPaths[randomIndex] = demand.UsedPaths[randomIndex] + 1;
                        
                    }
                }

            }
            return Population;
        }
        //Metoda odpowiadająca za krzyżowanie 
        public Solution Crossover(Solution parent1, Solution parent2)
        {
            Solution child1 = parent1;
            Solution child2 = parent2;
            return child1;
        }
        //Metoda odpowiadająca za mutację
        public Solution Mutation(Solution solutionForMute)
        {
            Solution mutadedSolution = solutionForMute;
            return mutadedSolution;
        }
        //Metoda odpowiedzialna za obliczanie przystosowania z funkcji celu
        public double CalculateAdaptation(Solution solutionForAdaptation)
        {
            double adaptation = 1.0;
            return adaptation;
        }
        //Metoda odpowiedzialna za wczytanie parametrow algorytmu
        public void LoadAlgorithmParameters()
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
                // switch (stopCrit)
                //{
                // case 1:
                // = StopCriterium.generationsNumber;
                //break;
                //}
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
            }
            catch
            {
                Console.WriteLine("Wrong input was provided. Please try again:");
                LoadAlgorithmParameters();
            }
            #endregion
        }
    }
}
