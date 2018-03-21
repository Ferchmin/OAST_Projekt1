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

        public Path(Path path)
        {
            this.Links = new List<Link>();

            foreach(Link link in path.Links)
            {
                Link copy = new Link(link);
                this.Links.Add(copy);
            }

        }
    }
}
