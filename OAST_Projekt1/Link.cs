using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAST_Projekt1
{
    class Link
    {
        public int id { get; set; }
        public int firstNode { get; set; }
        public int secondNode { get; set; }
        public int lambdas { get; set; }
        public int fibres { get; set; }
        public int cost { get; set; }
        public int capacity { get; set; }
        public int usedCapacity { get; set; } = 0;

        public Link(int id, int firstNode, int secondNode, int fibres, int cost, int lambdas)
        {
            this.id = id;
            this.firstNode = firstNode;
            this.secondNode = secondNode;
            this.cost = cost;
            this.lambdas = lambdas;
            this.fibres = fibres;
            capacity = fibres * lambdas;
        }
    }
}
