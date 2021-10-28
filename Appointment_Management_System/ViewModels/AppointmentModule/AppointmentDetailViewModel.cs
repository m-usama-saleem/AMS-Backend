using Appointment_Management_System.ViewModels.FinanceModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Appointment_Management_System.ViewModels.AppointmentModule
{
    public class AppointmentDetailViewModel
    {
        public AppointmentViewModel Appointment { get; set; }
        public FinanceViewModel Payable { get; set; }
        public FinanceViewModel Receivable { get; set; }
    }
}
