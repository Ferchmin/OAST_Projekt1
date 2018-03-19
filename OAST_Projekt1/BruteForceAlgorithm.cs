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

        Random rand;

        public BruteForceAlgorithm(List<Link> links, List<Demand> demands)
        {
            this.links = links;
            this.demands = demands;

            rand = new Random(2);

            solveBruteForceAlgorithm();
        }

        void solveBruteForceAlgorithm()
        {
            foreach (Link link in this.links)
            {
                link.usedCapacity = 0;
            }

            foreach(Demand demand in demands)
            {
                for(int i = 0; i < demand.demandedCapacity; i++)
                {
                    int randomPathIndex = rand.Next(demand.AvailablePaths.Count());
                    foreach(Link link in demand.AvailablePaths[randomPathIndex].Links)
                    {
                        Link networkLink = this.links.Find(x => x.id == link.id);
                        networkLink.usedCapacity += 1;
                    }
                }
            }

            foreach(Link link in links)
            {
                if(link.usedCapacity > 0){
                    Console.WriteLine(link.id + " " + link.usedCapacity);
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
    }
}
