using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAST_Projekt1
{
    class Demand
    {
        public int id { get; set; }
        public int firstNode { get; set; }
        public int secondNode { get; set; }
        public int demandedCapacity { get; set; }
        public List<Path> AvailablePaths { get; set; }
        public List<int> UsedPaths { get; set; }
        public List<List<int>> possibleSolutions = new List<List<int>>();

        public Demand(int id, int firstNode, int secondNode, int demandedCapacity, List<Path> AvailablePaths, List<int> UsedPaths)
        {
            this.id = id;
            this.firstNode = firstNode;
            this.secondNode = secondNode;
            this.demandedCapacity = demandedCapacity;
            this.AvailablePaths = AvailablePaths;
            this.UsedPaths = UsedPaths;
        }

        public Demand(int id, int firstNode, int secondNode, int demandedCapacity)
        {
            this.id = id;
            this.firstNode = firstNode;
            this.secondNode = secondNode;
            this.demandedCapacity = demandedCapacity;
            AvailablePaths = new List<Path>();
            UsedPaths = new List<int>();
        }

        public Demand(Demand demand)
        {
            this.id = demand.id;
            this.firstNode = demand.firstNode;
            this.secondNode = demand.secondNode;
            this.demandedCapacity = demand.demandedCapacity;

            this.AvailablePaths = new List<Path>();
            foreach(Path path in demand.AvailablePaths)
            {
                Path copy = new Path(path);
                AvailablePaths.Add(copy);
            }
  
            this.UsedPaths = new List<int>();
            foreach(int path in demand.UsedPaths)
            {
                UsedPaths.Add(path);
            }
        }
    }
}
