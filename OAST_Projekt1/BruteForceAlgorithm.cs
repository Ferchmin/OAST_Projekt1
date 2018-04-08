using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAST_Projekt1
{
    class BruteForceAlgorithm
    {
        List<Link> links;
        List<Demand> demands;
        Solution solution;

        TextWriter tw2 = new StreamWriter("bruteForceResult.txt", false);

        //List<List<int>> possibleCombinations = new List<List<int>>();

        public BruteForceAlgorithm(List<Link> links, List<Demand> demands)
        {
            this.links = links;
            this.demands = demands;
            this.solution = new Solution(demands,links);

            //Lista wszystkich kombinacji dla demandow
            var allPossibleCombinations = new List<List<List<int>>>();

            //Najwyzsza liczba mozliwych rozwiazan
            var highestNumberOfPossibleSolutions = 0;

            //Kombinacje z powtorzeniami dla kazdego pojedynczego demanda
            foreach (Demand demand in demands)
            {
                var input = new List<int>();
                for (int i = 0; i <= demand.demandedCapacity; i++) input.Add(i);
                var possibleCombinations = new List<List<int>>();
                possibleCombinations = preparePossibleSolutionsForDemand(input, demand.AvailablePaths.Count, demand.demandedCapacity);
                demand.possibleSolutions = possibleCombinations;
                if(possibleCombinations.Count > highestNumberOfPossibleSolutions)
                {
                    highestNumberOfPossibleSolutions = possibleCombinations.Count;
                }
            }
            List<int> inputForCombinations = new List<int>();
            for(int i = 0; i < highestNumberOfPossibleSolutions; i++)
            {
                inputForCombinations.Add(i);
            }
            
            var allPosibleCombinationsOfSolutions = prepareAllPossibleCombinationsOfSolutions(inputForCombinations, demands.Count);
            Console.WriteLine("liczba mozliwych rozwiazan: " + allPosibleCombinationsOfSolutions.Count);

            solveBruteForceAlgorithm(allPosibleCombinationsOfSolutions);
        }

        void solveBruteForceAlgorithm(List<List<int>> allPossibleSolutions)
        {
            foreach (Link link in this.links)
            {
                link.usedCapacity = 0;
            }
            
            foreach(var solutionList in allPossibleSolutions)
            {
                var solution = new Solution(demands, links);
                for(int i = 0; i < solution.Demands.Count; i++)
                {
                    solution.Demands[i].UsedPaths = solution.Demands[i].possibleSolutions[solutionList[i]];
                }
                solution.objectiveFunctionResult = solution.objectiveFunction();
                if(solution.objectiveFunctionResult == 0)
                {
                    solution.print();
                    tw2.WriteLine(solution.printToFile());
                    break;
                }
            }
        }


        List<List<int>> preparePossibleSolutionsForDemand(IEnumerable<int> input, int lenght,int demandSum)
        {
            var possibleCombinations = new List<List<int>>();
            var possibleCombinationsEnumerable = GetCombinations(input, lenght);
            foreach (var combination in possibleCombinationsEnumerable)
            {
                possibleCombinations.Add(combination.ToList());
            }
            var goodCombinations = new List<List<int>>();
            foreach(var combination in possibleCombinations)
            {
                var sum = 0;
                foreach(var number in combination)
                {
                    sum += number;
                }
                if(sum == demandSum)
                {
                    goodCombinations.Add(combination);
                }
            }

            return goodCombinations;
        }

        List<List<int>> prepareAllPossibleCombinationsOfSolutions(IEnumerable<int> input, int length)
        {
            var possibleCombinationsEnumerable = GetCombinations(input, length);
            //Zmieniam na liste
            var possibleCombinations = new List<List<int>>();
            foreach(var combination in possibleCombinationsEnumerable)
            {
                possibleCombinations.Add(combination.ToList());
            }

            //Sprawdzam czy kombinacja jest ok
            var goodCombinations = new List<List<int>>();
            foreach (var combination in possibleCombinations)
            {
                var isOK = true;
                for (int i = 0; i < length; i++)
                {
                    if (combination[i] >= demands[i].possibleSolutions.Count)
                    {
                        isOK = false;
                    }
                }
                if (isOK)
                {
                    goodCombinations.Add(combination);
                }
            }
            return goodCombinations;
        }

        int objectiveFunction()
        {
            List<int> linkResults = new List<int>();
            foreach(Link link in links)
            {
                linkResults.Add(Math.Max(0,link.usedCapacity - link.capacity));
            }

            return Math.Max(0, linkResults.Max());
        }

        static IEnumerable<IEnumerable<T>> GetCombinations<T>(IEnumerable<T> list, int length)
        {
            if (length == 1) return list.Select(t => new T[] { t });

            return GetCombinations(list, length - 1)
                .SelectMany(t => list, (t1, t2) => t1.Concat(new T[] { t2 }));
        }

    }
}
