// See https://aka.ms/new-console-template for more information

using Lesson12_14Proj.Repositories;
using Lesson12_14Proj.Service;

GateChecker gateCheck = new GateChecker(new WorkerRepository(), new GateRepository(), new WorkersInWorkPlaceRepository());

gateCheck.GateCheckEvent(1, 1, DateTime.Now);


/*

string filePath = @"C:\Users\tomas.ceida\source\repos\Lesson12-14Proj\Lesson12-14Proj\Data\WorkersInWorkPlace.txt";

string[] lines = File.ReadAllLines(filePath);

Console.WriteLine(lines[0]);
foreach (string ids in lines[0].Split(';'))
{
    
    Console.WriteLine(ids);
}
*/