using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAST_Projekt1
{
    class Link
    {
        int id { get; set; }
        int firstNode { get; set; }
        int secondNode { get; set; }
        int lambdas { get; set; }
        int fibres { get; set; }
        int cost { get; set; }
        int capacity { get; set; }

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
