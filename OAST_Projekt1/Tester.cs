using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAST_Projekt1
{
    class Tester
    {
        string filepath;

        Link link;
        Demand demand;
        Path path;
        List<Link> Links = new List<Link>();
        List<Demand> Demands = new List<Demand>();
        List<Link> PathsLinks;
        List<int> LinksId;

        StreamReader streamReader;

        public Tester()
        {
            ReadInputFile();
            //ChooseProgramMode();
            // LoadAlgorithmParameters();
        }

        public void ReadInputFile()
        {
            Console.WriteLine("Drag and drop input file and press ENTER to load data for algorithm");
            try
            {
                filepath = Console.ReadLine();
                filepath = "Network.txt";
                streamReader = new StreamReader(filepath);
            }
            catch
            {
                Console.WriteLine("Unable to load desired file. Please try again:");
                ReadInputFile();
            }

            //READING LINKS
            int linksAmmount = Int32.Parse(streamReader.ReadLine());
            for (int i = 1; i <= linksAmmount; i++)
            {
                string parsedString = streamReader.ReadLine();
                string[] splittedLine = parsedString.Split(' ');
                int[] linkParameters = Array.ConvertAll(splittedLine, int.Parse);
                Links.Add(link = new Link(i, linkParameters[0], linkParameters[1], linkParameters[2], linkParameters[3], linkParameters[4]));
            }
            streamReader.ReadLine();
            streamReader.ReadLine();
            //READING DEMANDS
            int demandsAmmount = Int32.Parse(streamReader.ReadLine());
            streamReader.ReadLine();
            for (int i = 1; i <= demandsAmmount; i++)
            {
                string parsedString = streamReader.ReadLine();
                string[] splittedLine = parsedString.Split(' ');
                int[] demandParameters = Array.ConvertAll(splittedLine, int.Parse);
                Demands.Add(demand = new Demand(i, demandParameters[0], demandParameters[1], demandParameters[2]));

                int pathsForDemandsAmmount = Int32.Parse(streamReader.ReadLine());
                for (int j = 1; j <= pathsForDemandsAmmount; j++)
                {
                    string parsedStringD = streamReader.ReadLine();
                    string[] splittedLineD = parsedStringD.Split(' ');
                    int[] pathDemandParameters = Array.ConvertAll(splittedLineD, int.Parse);

                    //LinksId = new List<int>();
                    PathsLinks = new List<Link>();
                    for (int k = 1; k < pathDemandParameters.Length; k++)
                    {

                        // LinksId.Add(pathDemandParameters[k]);
                        PathsLinks.Add(Links[pathDemandParameters[k] - 1]);

                    }
                    //demand.AvailablePaths.Add(path = new Path(LinksId));
                    demand.AvailablePaths.Add(path = new Path(PathsLinks));

                }
                streamReader.ReadLine();
            }
            streamReader.Close();


        }



        public void ChooseProgramMode()
        {
            Console.WriteLine("Choose program mode:");
            Console.WriteLine("Choose program mode:\n{0}\n{1}\n}",
                "1 - brute-force",
                "2 - evolution algorithm");
            int mode = Int32.Parse(Console.ReadLine());
            switch (mode)
            {
                case 1:
                    BruteForce(Links, Demands);
                    break;
                case 2:
                    EvolutionAlgorithm evolutionAlgorithm = new EvolutionAlgorithm(Links, Demands);
                    break;
            }
        }

        public void BruteForce(List<Link> links, List<Demand> demands)
        {
            Console.WriteLine("You have chosen brute-force algorithm");
        }
    }
}
