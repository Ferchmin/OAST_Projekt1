using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAST_Projekt1
{
    class Solution
    {
        public List<Demand> Demands = new List<Demand>();
        public List<int> UsedPaths = new List<int>();
        // Dictionary<Demand, List<int>> UsedPathsNumberForDemand = new Dictionary<Demand, List<int>>();

        public Solution(List<Demand> Demands)
        {
            foreach(Demand demand in Demands)
            {
                Demand copy = new Demand(demand);
                this.Demands.Add(copy);
            }
        }
    }
 }

    

