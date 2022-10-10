using Lesson12_14Proj.Classes;
using Lesson12_14Proj.Repositories;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson12_14Proj.Service
{
    public class ReportGenerator
    {
        public WorkerRepository Workers {get; set;}
        public EventRepository EventsLogs { get; set;}
        public List<ReportItem> ReportsData { get; set;}

        public ReportGenerator(WorkerRepository workers, EventRepository eventsLogs)
        {
            Workers = workers;
            EventsLogs = eventsLogs;
            ReportsData = new List<ReportItem>();
            EventsLogs.OrderEventsByDate();
            List<int> eventIDsList = new List<int>();
            foreach (Event eventItem in EventsLogs.EventList)
            {
                if (!eventIDsList.Contains(eventItem.EntranceID) && eventItem.EntranceID != -1)
                {
                    eventIDsList.Add(eventItem.EntranceID);
                    ReportsData.Add(
                        new ReportItem(
                            eventItem.WorkerID,
                            eventItem.EntranceID,
                            EventsLogs.EventList.Where(evn => evn.EntranceID == eventItem.EntranceID).Min(evn => evn.EventTime),
                            EventsLogs.EventList.Where(evn => evn.EntranceID == eventItem.EntranceID).Max(evn => evn.EventTime)
                                )
                        );
                }

            }
        }

        public void GenerateWorkerReport(int workerID)
        {
            List<ReportItem> workerData = ReportsData.Where(r => r.EmployeeID == workerID).ToList();
            List<string> uniquePeriods = workerData.Select(e => e.YYYY_MM).Distinct().ToList();
            List<string> reportStringList = new List<string>();
            reportStringList.Add("yyyy-mm;WorkerName;NoOfDays;TotalHours;Salary;");
            string WorkerName;
            int NoOfDays;
            double TotalHours;
            decimal Salary;
            foreach (string periodName in uniquePeriods)
            {
                WorkerName = Workers.GetWorker(workerID).WorkerName;
                NoOfDays = workerData.Where(evn => evn.YYYY_MM == periodName).ToList().Count();
                TotalHours = workerData.Where(evn => evn.YYYY_MM == periodName).Sum(evn => evn.WorkHours);
                Salary = (decimal)TotalHours * Workers.GetWorker(workerID).HourlyRate;
                Console.WriteLine($"{periodName};{WorkerName};{NoOfDays};{TotalHours};{Salary};");
            }

        }
    }
}
