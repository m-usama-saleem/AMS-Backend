using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Appointment_Management_System.ViewModels.AppointmentModule
{
    public class AppointmentViewModel
    {
        public Int64 Id { get; set; }
        public String AppointmentId { get; set; }
        public Int64 TranslatorId { get; set; }
        public String TranslatorName { get; set; }
        public Int64 InstitutionId { get; set; }
        public Int64 CreatedBy { get; set; }
        public String InstitutionName { get; set; }
        public String Type { get; set; }
        public DateTime EntryDate { get; set; }
        public DateTime AppointmentDate { get; set; }
        public Decimal Tax { get; set; }
        public Decimal Rate { get; set; }
        public Decimal Hours { get; set; }
        public Decimal Discount { get; set; }
        public Decimal NetPayment { get; set; }
        public String Status { get; set; }
        public String IsDeleted { get; set; }
        public String Attachments { get; set; }
        public String Language { get; set; }
    }
}
