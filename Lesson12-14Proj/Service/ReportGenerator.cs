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
        public List<ReportItem> ReportDataUnauthEvents { get; set;}

        public ReportGenerator(WorkerRepository workers, EventRepository eventsLogs)
        {
            Workers = workers;
            EventsLogs = eventsLogs;
            ReportsData = new List<ReportItem>();
            ReportDataUnauthEvents = new List<ReportItem>();
            EventsLogs.OrderEventsByDate();
            List<int> eventIDsList = new List<int>();
            foreach (Event eventItem in EventsLogs.EventList)
            {
                if (!eventIDsList.Contains(eventItem.EntranceID))
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
            string WorkerName = Workers.GetWorker(workerID).WorkerName;
            int NoOfDays;
            int TotalEvents;
            int UnauthEvents;
            double UnauthEventsRatio;
            double TotalHours;
            decimal Salary;
            DateTime currDate = DateTime.Now;
            string HTMLPath = $@"C:\Users\tomas.ceida\source\repos\Lesson12-14Proj\Lesson12-14Proj\Reports\Report_Worker_{workerID}_{currDate.ToString("yyyy'-'MM'-'dd'T'HH'_'mm'_'ss")}.html";

            string HTMLUpperPart = $@"
                <!DOCTYPE html>
                <html>
                <body>
                <h1>Worker {WorkerName} Report {currDate.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss")} </h1>
                <table>
                  <tr>
                    <th>Date</th>
                    <th>WorkerName</th>
                    <th>NoOfDays</th>
                    <th>UnAuthEventsRatio</th>
                    <th>TotalHours</th>
                    <th>Salary</th>
                  </tr>";


            string HTMLLowerPart = @"
                </table>
                </body>
                </html>
                ";
            string HTMLTable = "";

            foreach (string periodName in uniquePeriods)
            {
                NoOfDays = workerData.Where(evn => evn.YYYY_MM == periodName && evn.EntranceID != -1).ToList().Count();             
                UnauthEvents = workerData.Where(evn => evn.YYYY_MM == periodName && evn.EntranceID == -1).ToList().Count();
                
                
                TotalEvents = (NoOfDays * 2) + UnauthEvents;
                if (TotalEvents != 0)
                {
                    UnauthEventsRatio = (double)UnauthEvents / (double)TotalEvents;
                }
                else
                {
                    UnauthEventsRatio = 0;
                }
                UnauthEventsRatio = 
                TotalHours = workerData.Where(evn => evn.YYYY_MM == periodName).Sum(evn => evn.WorkHours);
                Salary = (decimal)TotalHours * (decimal)Workers.GetWorker(workerID).HourlyRate;
                HTMLTable += $@"
                <tr>
                <td>{periodName}</td>
                <td>{WorkerName}</td>
                <td>{NoOfDays}</td>
                <td>{Math.Round(UnauthEventsRatio, 2)} % </td>
                <td>{Math.Round(TotalHours, 2)}</td>
                <td>{Math.Round(Salary, 2)}</td>
                </tr>
                ";
            }
            System.IO.File.WriteAllText(HTMLPath, HTMLUpperPart + HTMLTable + HTMLLowerPart);

        }
        
        public void GenerateAllReports()
        {
            foreach (Worker worker in Workers.WorkerList)
            {
                GenerateWorkerReport(worker.WorkerID);
            }
        }

        public void DeleteAllReports()
        {
            DirectoryInfo DirectoryObj = new DirectoryInfo("C:\\Users\\tomas.ceida\\source\\repos\\Lesson12-14Proj\\Lesson12-14Proj\\Reports\\");
            foreach (FileInfo file in DirectoryObj.EnumerateFiles())
            {
                file.Delete();
            }
        }

    }
}
