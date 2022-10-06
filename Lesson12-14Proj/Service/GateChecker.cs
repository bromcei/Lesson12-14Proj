using Lesson12_14Proj.Classes;
using Lesson12_14Proj.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson12_14Proj.Service
{
    public class GateChecker
    {
        public int EntranceID { get; set; }
        public WorkerRepository Workers { get; set; }
        public GateRepository Gates { get; set; }
        //    public EventRepository Events { get; set; }
        public WorkersInWorkPlaceRepository WorkersInWorkPlace { get; set; }
        public string EventLogFilePath {get;}
        public GateChecker(WorkerRepository workers, GateRepository gates, WorkersInWorkPlaceRepository workersInWorkPlace)
        {
            Workers = workers;
            Gates = gates;
        //    Events = events;
            EntranceID = 0;
            WorkersInWorkPlace = workersInWorkPlace;
            EventLogFilePath = @"C:\Users\tomas.ceida\source\repos\Lesson12-14Proj\Lesson12-14Proj\Data\EventLog.txt";
        }

        public void WorkersInWorkPlacerefresh() 
        {
            WorkersInWorkPlace = new WorkersInWorkPlaceRepository();
        }
        public bool GateCheckEvent(int workerID, int gateID, DateTime eventTime)
        {
            string eventName;
            string eventString;
            string workPlaceEntranceString;
            WorkersInWorkPlacerefresh();
            List<string> workerInWorkPlaceNewList = new List<string>();
            string stringToRemove;
            Console.WriteLine($"Worker ID: {workerID}, Name: {Workers.GetWorker(workerID).WorkerName}, Access gate: {Workers.GetWorker(workerID).GateNumber}");
            Console.WriteLine($"Trying to access gate No: {gateID}");
            if (WorkersInWorkPlace.WorkersInWorkPlaceDict.ContainsKey(workerID))
            {
                eventName = "Exit";
            }
            else if (Workers.GetWorker(workerID).GateNumber == gateID)
            {

                eventName = "Entrance";
            }
            else
            {
                eventName = "Access Denied";
            }

            if (eventName == "Entrance")
            {
                EntranceID = File.ReadAllLines(WorkersInWorkPlace.EntranceIDstxtPath).Length + 1;
                //EventList.Add(new Event(workerID, line.Split(";")[1], eventTime, gateiD, entranceID));
                eventString = $"{workerID};{eventName};{eventTime};{gateID};{EntranceID};";
                workPlaceEntranceString = $"{workerID};{EntranceID};";
               
                File.AppendAllText(EventLogFilePath, eventString + Environment.NewLine);
                File.AppendAllText(WorkersInWorkPlace.WorkersInWorkPlacetxtPath, workPlaceEntranceString + Environment.NewLine);
                File.AppendAllText(WorkersInWorkPlace.EntranceIDstxtPath, $"{EntranceID}" + Environment.NewLine);
                Console.WriteLine($"New Event: {eventString}");
                return true;

            }
            else if (eventName == "Exit")
            {
                EntranceID = WorkersInWorkPlace.WorkersInWorkPlaceDict[workerID];
                eventString = $"{workerID};{eventName};{eventTime};{gateID};{EntranceID};";
                File.AppendAllText(EventLogFilePath, eventString + Environment.NewLine);
                
                stringToRemove = $"{workerID};{EntranceID};";

                foreach (string line in File.ReadAllLines(WorkersInWorkPlace.WorkersInWorkPlacetxtPath))
                {
                    if (stringToRemove != line)
                    {
                        workerInWorkPlaceNewList.Add(line);
                    }
                }
                File.WriteAllText(WorkersInWorkPlace.WorkersInWorkPlacetxtPath, String.Empty);
                foreach (string line in workerInWorkPlaceNewList)
                {
                    File.AppendAllText(WorkersInWorkPlace.WorkersInWorkPlacetxtPath, line + Environment.NewLine);
                }
                Console.WriteLine($"New Event: {eventString}");
                return true;

            }
            else if (eventName == "Access Denied")
            {
                eventString = $"{workerID};{eventName};{eventTime};{gateID};-1";
                File.AppendAllText(EventLogFilePath, eventString + Environment.NewLine);
                Console.WriteLine($"New Event: {eventString}");

                return false;
            }
            else
            {
                return false;
            }


        }
    }
}
