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
        public int objectiveFunctionResult;
        // Dictionary<Demand, List<int>> UsedPathsNumberForDemand = new Dictionary<Demand, List<int>>();

        public Solution(List<Demand> Demands)
        {
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
            this.objectiveFunctionResult = solution.objectiveFunctionResult;
            foreach (Demand demands in solution.Demands)
            {
                Demand copy = new Demand(demands);
                this.Demands.Add(copy);
            }
        }
    }
 }

    

