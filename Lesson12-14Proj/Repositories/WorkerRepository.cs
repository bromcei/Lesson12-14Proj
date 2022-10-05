﻿using Lesson12_14Proj.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson12_14Proj.Repositories
{
    public class WorkerRepository
    {
        public List<Worker> WorkerList { get; set; }

        public WorkerRepository()
        {
            WorkerList = new List<Worker>();
            WorkerList.Add(new Worker(1, "Petras", 1, true));
            WorkerList.Add(new Worker(2, "Antanas", 3, true));
            WorkerList.Add(new Worker(3, "Jurgis", 2, true));
            WorkerList.Add(new Worker(4, "Jonas", 1, true));
            WorkerList.Add(new Worker(5, "Tomas", 2, true));
            WorkerList.Add(new Worker(6, "Darius", 4, true));
            WorkerList.Add(new Worker(7, "Laurynas", 3, true));
            WorkerList.Add(new Worker(8, "Mindaugas", 4, true));
            WorkerList.Add(new Worker(9, "Karolis", 2, true));
            WorkerList.Add(new Worker(10, "Nerijus", 4, true));
            WorkerList.Add(new Worker(11, "Joe", 4, false));
            WorkerList.Add(new Worker(12, "Romas", 3, true));

        }
        public Worker GetWorker(int workerID)
        {
            return WorkerList.Where(w => w.WorkerID == workerID).FirstOrDefault();
        }
    }
}
