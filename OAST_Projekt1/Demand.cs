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
        public List<Path> AvailablePaths = new List<Path>();
        public List<int> UsedPaths = new List<int>();

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

        }
    }
}
