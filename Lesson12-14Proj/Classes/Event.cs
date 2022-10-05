using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson12_14Proj.Classes
{
    public class Event
    {
        public int WorkerID { get; set; }
        public string EventName { get; set; }
        public DateTime EventTime { get; set; }
        public int GateID { get; set; }
        public int EntranceID { get; set; }
        public Event(int workerID, string eventName, DateTime eventTime, int gateiD, int entranceID)
        {
            WorkerID = workerID;
            EventName = eventName;
            EventTime = eventTime;
            GateID = gateiD;
            EntranceID = entranceID;
        }
    }
 
}
