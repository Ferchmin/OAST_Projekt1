using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAST_Projekt1
{
    class Solution
    {
        public List<Link> links = new List<Link>();
        public List<Demand> Demands = new List<Demand>();
        public int objectiveFunctionResult;

        public Solution(List<Demand> Demands, List<Link> links)
        {
            this.links = links;
            foreach (Demand demand in Demands)
            {
                Demand copy = new Demand(demand);
                this.Demands.Add(copy);
            }
        }

        public Solution()
            {
             this.Demands=new List<Demand>();
            }

        public Solution(Solution solution)
        {
            this.links = solution.links;
            this.objectiveFunctionResult = solution.objectiveFunctionResult;
            foreach (Demand demands in solution.Demands)
            {
                Demand copy = new Demand(demands);
                this.Demands.Add(copy);
            }
        }

        public void print()
        {
            Console.WriteLine("Objective function result: " + objectiveFunctionResult);
            List<int> usedLinks = new List<int>();
            foreach (Link link in links)
            {
                usedLinks.Add(0);
            }
            foreach(Demand demand in Demands)
            {
                for(int i=0;i < demand.AvailablePaths.Count; i++)
                {
                    if(demand.UsedPaths[i] != 0)
                    {
                        foreach(Link link in demand.AvailablePaths[i].Links)
                        {
                            usedLinks[link.id - 1] = usedLinks[link.id - 1] + demand.UsedPaths[i];
                        }
                    }
                }
            }
            Console.WriteLine("Used links: ");
            for(int i = 0; i < usedLinks.Count; i++)
            {
                Console.WriteLine("Link #" + (i + 1) + ": " + usedLinks[i]);
            }
        }

        public int objectiveFunction()
        {
            List<int> usedLinks = new List<int>();
            foreach (Link link in links)
            {
                usedLinks.Add(0);
            }
            foreach (Demand demand in Demands)
            {
                for (int i = 0; i < demand.AvailablePaths.Count; i++)
                {
                    if (demand.UsedPaths[i] != 0)
                    {
                        foreach (Link link in demand.AvailablePaths[i].Links)
                        {
                            usedLinks[link.id - 1] = usedLinks[link.id - 1] + demand.UsedPaths[i];
                        }
                    }
                }
            }
            List<int> linkResults = new List<int>();

            for (int i = 0; i < links.Count; i++)
            {
                linkResults.Add(Math.Max(usedLinks[i], links[i].capacity));
            }
            return Math.Max(0, linkResults.Max());
        }

        public String printToFile()
        {
            String textToPrint = "";
            textToPrint += "Objective function result:" + objectiveFunctionResult + Environment.NewLine;
            List<int> usedLinks = new List<int>();
            foreach (Link link in links)
            {
                usedLinks.Add(0);
            }
            foreach (Demand demand in Demands)
            {
                for (int i = 0; i < demand.AvailablePaths.Count; i++)
                {
                    if (demand.UsedPaths[i] != 0)
                    {
                        foreach (Link link in demand.AvailablePaths[i].Links)
                        {
                            usedLinks[link.id - 1] = usedLinks[link.id - 1] + demand.UsedPaths[i];
                        }
                    }
                }
            }
            textToPrint += "Used links: " + Environment.NewLine;
            for (int i = 0; i < usedLinks.Count; i++)
            {
                textToPrint += "Link #" + (i + 1) + ": " + usedLinks[i] + Environment.NewLine;
            }
            return textToPrint;
        }
    }
 }

    

