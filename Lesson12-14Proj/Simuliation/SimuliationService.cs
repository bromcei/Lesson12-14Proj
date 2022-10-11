using Lesson12_14Proj.Classes;
using Lesson12_14Proj.Repositories;
using Lesson12_14Proj.Service;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace Lesson12_14Proj.Simuliation
{
    public class SimuliationService
    {
        public List<DateTime> SimDateList { get; set; }
        public List<int> SimStartHoursRange { get; set; }
        public List<int> SimEndHoursRange { get; set; }
        public GateChecker GateService { get; set; }
        public WorkerRepository Workers{ get; set; }
        public WorkersInWorkPlaceRepository WorkersInWorkPlace { get; set; }
        public SimuliationService(GateChecker gateService, string startDate, string endDate)
        {
            GateService = gateService;
            Workers = GateService.Workers;
            WorkersInWorkPlace = GateService.WorkersInWorkPlace;
            SimDateList = new List<DateTime>();
            for (var dt = DateTime.Parse(startDate); dt <= DateTime.Parse(endDate); dt = dt.AddDays(1))
            {
                SimDateList.Add(dt);
            }
            SimStartHoursRange = new List<int> { 7, 8, 9, 10 };
            SimEndHoursRange = new List<int> { 15, 16, 17, 18 };
            

        }
        public void ClearEventsData()
        {
            File.WriteAllText(GateService.EventLogFilePath, string.Empty);
            File.WriteAllText(WorkersInWorkPlace.EntranceIDstxtPath, string.Empty);
            File.WriteAllText(WorkersInWorkPlace.WorkersInWorkPlacetxtPath, string.Empty);
        }
        public void SimulationStart()
        {
            List<Worker> workerListToStartWork = new List<Worker>();
            List<Worker> workerListToEndWork = new List<Worker>();
            List<int> PosibleWorkerGates;
            string eventTimeStr;
            DateTime eventTime;
            Random random = new Random();
            bool succesExit;
            foreach (DateTime simDate in SimDateList)
            {
                //Workers entrance simuliation
                workerListToStartWork = Workers.WorkerList.OrderBy(arg => Guid.NewGuid()).Take(4).ToList();

                foreach (Worker worker in workerListToStartWork)
                {
                    succesExit = false;
                    PosibleWorkerGates = new List<int>(){ worker.GateNumber, worker.GateNumber, worker.GateNumber, new Random().Next(4) };
                    //list[random.Next(list.Count)];


                    while (succesExit == false)
                    {
                        eventTimeStr = $"{simDate.ToString("yyyy-MM-dd")} {SimStartHoursRange[random.Next(SimStartHoursRange.Count)]}:{random.Next(60)}";
                        eventTime = DateTime.Parse(eventTimeStr);
                        succesExit = GateService.GateCheckEvent(worker.WorkerID, PosibleWorkerGates[random.Next(PosibleWorkerGates.Count)], eventTime);
                    }

                }


                //Workers leave work
                foreach (Worker worker in workerListToStartWork)
                {

                    succesExit = false;
                    PosibleWorkerGates = new List<int>() { worker.GateNumber, worker.GateNumber, worker.GateNumber, new Random().Next(4) };
                    //list[random.Next(list.Count)];

                    while (succesExit == false)
                    {                        
                        eventTimeStr = $"{simDate.ToString("yyyy-MM-dd")} {SimEndHoursRange[random.Next(SimStartHoursRange.Count)]}:{random.Next(60)}";
                        eventTime = DateTime.Parse(eventTimeStr);
                        succesExit = GateService.GateCheckEvent(worker.WorkerID, PosibleWorkerGates[random.Next(PosibleWorkerGates.Count)], eventTime);
                    }

                }


            }
        }
    }
}
