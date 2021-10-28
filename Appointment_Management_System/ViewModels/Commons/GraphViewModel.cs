using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Appointment_Management_System.ViewModels.Commons
{
    public class PieChartViewModel
    {
        public string name { get; set; }
        public int value { get; set; }
    }

    public class BarChartViewModel
    {
        public string name { get; set; }
        public string type { get; set; }
        public int value1 { get; set; }
        public int value2 { get; set; }
    }
}
