using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAST_Projekt1
{
    class Path
    {
        public List<Link> Links = new List<Link>();

        public Path(List<Link> links)
        {
            this.Links = links;
        }
    }
}
