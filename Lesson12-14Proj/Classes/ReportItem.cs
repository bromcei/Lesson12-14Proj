using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson12_14Proj.Classes
{
    public class ReportItem
    {

        string EmployeeName { get; set; }
        string MonthName { get; set; }  
        int NoOfDaysWorked { get; set; }
        decimal HoursWorked { get; set; }
        decimal HourlyRate { get; set; }
        decimal Salary { get; set; }

        public ReportItem (string employeeName, string monthName, int noOfDaysWorked, decimal hoursWorked, decimal hourlyRate, decimal salary)
        {
            EmployeeName = employeeName;
            MonthName = monthName;
            NoOfDaysWorked = noOfDaysWorked;
            HoursWorked = hoursWorked;
            HourlyRate = hourlyRate;
            Salary = salary;
        }
    }
}
