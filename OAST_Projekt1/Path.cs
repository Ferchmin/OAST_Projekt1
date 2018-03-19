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
        public List<int> LinksId = new List<int>();

        public Path(List<Link> links)
        {
            this.Links = links;
        }

        public Path(List<int> LinksId)
        {
            this.LinksId = LinksId;
        }
    }
}
