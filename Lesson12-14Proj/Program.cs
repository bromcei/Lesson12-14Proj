using Lesson12_14Proj.Repositories;
using Lesson12_14Proj.Service;
using Lesson12_14Proj.Simuliation;

WorkerRepository WorkersRep = new WorkerRepository();
WorkersInWorkPlaceRepository WorkersInWorkPlaceRep = new WorkersInWorkPlaceRepository();
GateRepository GateRep = new GateRepository();
EventRepository EventsRep = new EventRepository();

GateChecker gateCheck = new GateChecker(WorkersRep, GateRep, WorkersInWorkPlaceRep);
SimuliationService sim = new SimuliationService(gateCheck, "2022-10-06", "2023-10-05");
sim.ClearEventsData();
sim.SimulationStart();


ReportGenerator reportGen = new ReportGenerator(WorkersRep, EventsRep);
reportGen.DeleteAllReports();
reportGen.GenerateAllReports();
