using System;
using System.Collections.Generic;
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

        List<List<int>> possibleCombinations = new List<List<int>>();

        public BruteForceAlgorithm(List<Link> links, List<Demand> demands)
        {
            this.links = links;
            this.demands = demands;
            this.solution = new Solution(demands,links);
            solveBruteForceAlgorithm();
        }


        List<List<int>> prepareAllPossibleCombinations(IEnumerable<int> input, int lenght,int demandSum)
        {
            var possibleScenarios = new List<List<int>>();
            foreach (var c in CombinationsWithRepition(input, lenght))
            {
                var intArray = new List<int>();
                foreach (char character in c)
                {
                    int tmpInt = int.Parse(character.ToString());
                    intArray.Add(tmpInt);
                }
                var sum = 0;
                foreach (int number in intArray)
                {
                    sum += number;
                }
                if (sum == demandSum)
                {
                    possibleCombinations.Add(intArray);
                }
            }
            return possibleCombinations;
        }

        void prepareAllPossibleSolutions(List<Demand> demands)
        {
            List<Solution> possibleSoltions = new List<Solution>();

            foreach (Demand demand in demands)
            {
                var input = new List<int>();
                for (int i = 0; i <= demand.demandedCapacity; i++) input.Add(i);
                var possibleCombinations = prepareAllPossibleCombinations(input, demand.AvailablePaths.Count, demand.demandedCapacity);
                demand.possibleSolutions = possibleCombinations;
            }



        }

        void solveBruteForceAlgorithm()
        {
            foreach (Link link in this.links)
            {
                link.usedCapacity = 0;
            }

            var allPossibleCombinations = new List<List<List<int>>>();


            foreach(Demand demand in demands)
            {
                var input = new List<int>();
                for(int i = 0; i <= demand.demandedCapacity; i++) input.Add(i);
                var possibleCombinations = prepareAllPossibleCombinations(input, demand.AvailablePaths.Count, demand.demandedCapacity);

                foreach(List<int> combination in possibleCombinations)
                {
                    for (int i = 0; i < demand.AvailablePaths.Count; i++)
                    {
                        foreach(Link link in demand.AvailablePaths[i].Links)
                        {
                            Link networkLink = this.links.Find(x => x.id == link.id);
                            networkLink.usedCapacity = networkLink.usedCapacity + 1 * combination[i];
                        }
                    }
                    if(objectiveFunction() == 0){
                        Console.WriteLine("Demand #" + demand.id);
                        for(int i=1;i<=combination.Count;i++)
                        {
                            Console.WriteLine("Path #"+i+" Used: "+combination[i-1]);
                        }
                        break;
                    }
                }
             
            }
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

        static IEnumerable<String> CombinationsWithRepition(IEnumerable<int> input, int length)
        {
            if (length <= 0)
                yield return "";
            else
            {
                foreach (var i in input)
                    foreach (var c in CombinationsWithRepition(input, length - 1))
                        yield return i.ToString() + c;
            }
        }

    }
}
