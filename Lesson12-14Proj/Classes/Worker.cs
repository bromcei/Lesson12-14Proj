using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson12_14Proj.Classes
{
    public class Worker
    {
        public int WorkerID { get; set; }
        public string WorkerName { get; set; }  
        public int GateNumber { get; set; }
        bool IsActive { get; set; }

        public Worker(int workerID, string workerName, int gateNumber, bool isActive)
        {
            WorkerID = workerID;
            WorkerName = workerName;
            GateNumber = gateNumber;
            IsActive = isActive;
        }

        public void ChangeGate(int newgateNumber)
        {
            GateNumber = newgateNumber;
        }
    }
}
