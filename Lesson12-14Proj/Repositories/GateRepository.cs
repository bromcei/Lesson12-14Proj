using Lesson12_14Proj.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson12_14Proj.Repositories
{
    public class GateRepository
    {
        public List<Gate> GateList { get; set; }

        public GateRepository()
        {
            GateList = new List<Gate>();
            GateList.Add(new Gate(1));
            GateList.Add(new Gate(2));
            GateList.Add(new Gate(3));
            GateList.Add(new Gate(4));
        }
    }
}
