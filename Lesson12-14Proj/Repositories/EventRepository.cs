using Lesson12_14Proj.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Lesson12_14Proj.Repositories
{
    public class EventRepository
    {
        public List<Event> EventList { get; set; }
        public string EventLogtxtPath { get; set; }
        string[] RawEventsLogFile { get; set; }
        public EventRepository()
        {
            EventList = new List<Event>();
            EventLogtxtPath = @"C:\Users\tomas.ceida\source\repos\Lesson12-14Proj\Lesson12-14Proj\Data\EventLog.txt";
            int workerID;
            string eventName;
            DateTime eventTime;
            int gateiD;
            int entranceID;
            string[] allPossibleEventsArray = { "Entrance", "Exit", "Access Denied" };
            List<string> allPossibleEventsList = new List<string>();
            RawEventsLogFile = File.ReadAllLines(EventLogtxtPath);

            foreach (string line in RawEventsLogFile)
            {
                if (
                    int.TryParse(line.Split(";")[0], out workerID) &&
                    allPossibleEventsArray.Contains(line.Split(";")[1]) &&
                    DateTime.TryParse(line.Split(";")[2], out eventTime) &&
                    int.TryParse(line.Split(";")[3], out gateiD) &&
                    int.TryParse(line.Split(";")[4], out entranceID)
                    )
                {
                    EventList.Add(new Event(workerID, line.Split(";")[1], eventTime, gateiD, entranceID));
                }
            }
        }
        public void NewEvent(int workerID, string eventName, DateTime eventTime, int gateiD, int entranceID)
        {
            EventList.Add(new Event(workerID, eventName, eventTime, gateiD, entranceID));
        }
    }
}
