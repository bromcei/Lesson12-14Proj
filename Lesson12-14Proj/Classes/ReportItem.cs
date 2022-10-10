using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson12_14Proj.Classes
{
    public class ReportItem
    {
        public int EmployeeID { get; set; }
        public string YYYY_MM { get; set; }
        public int EntranceID { get; set; }
        public DateTime EntranceDate { get; set; }
        public DateTime ExitDate { get; set; }

        public double WorkHours { get; set; }

       public ReportItem(int employeeID, int entranceID, DateTime entranceDate, DateTime exitDate)
        {
            EmployeeID = employeeID;
            EntranceID = entranceID;
            EntranceDate = entranceDate;
            ExitDate = exitDate;
            YYYY_MM = ExitDate.ToString("yyyy-MM");
            WorkHours = (ExitDate - EntranceDate).TotalHours;
        }


    }
}
