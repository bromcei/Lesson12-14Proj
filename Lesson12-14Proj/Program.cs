// See https://aka.ms/new-console-template for more information

using Lesson12_14Proj.Repositories;
using Lesson12_14Proj.Service;
using Lesson12_14Proj.Simuliation;


GateChecker gateCheck = new GateChecker(new WorkerRepository(), new GateRepository(), new WorkersInWorkPlaceRepository());
SimuliationService sim = new SimuliationService(gateCheck, "2022-10-06", "2023-10-05");
sim.ClearEventsData();
sim.SimulationStart();


ReportGenerator reportGen = new ReportGenerator(new WorkerRepository(), new EventRepository());
reportGen.DeleteAllReports();
reportGen.GenerateAllReports();
