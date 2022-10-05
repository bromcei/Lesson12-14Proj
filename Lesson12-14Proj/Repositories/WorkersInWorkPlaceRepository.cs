using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson12_14Proj.Repositories
{
    public class WorkersInWorkPlaceRepository
    {
        public int EntranceID { get; set; }
        public string EntranceIDstxtPath { get; set; }
        public string WorkersInWorkPlacetxtPath { get; set; }
        public Dictionary<int, int> WorkersInWorkPlaceDict { get; set; }

        public WorkersInWorkPlaceRepository()
        {
            EntranceIDstxtPath = @"C:\Users\tomas.ceida\source\repos\Lesson12-14Proj\Lesson12-14Proj\Data\EntranceIDs.txt";
            WorkersInWorkPlacetxtPath = @"C:\Users\tomas.ceida\source\repos\Lesson12-14Proj\Lesson12-14Proj\Data\WorkersInWorkPlace.txt";
            WorkersInWorkPlaceDict = new Dictionary<int, int>();

            int workerID;
            int workPlaceEntranceID;
            if (File.ReadAllLines(EntranceIDstxtPath).Length > 0)
            {
                foreach (string line in File.ReadAllLines(WorkersInWorkPlacetxtPath))
                {
                    Console.WriteLine(line.Split(";"));
                    if (int.TryParse(line.Split(";")[0], out workerID) && int.TryParse(line.Split(";")[1], out workPlaceEntranceID))
                    {
                        WorkersInWorkPlaceDict.Add(workerID, workPlaceEntranceID);
                    }
                }
            }

        }
    }
}
