using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Appointment_Management_System.ViewModels.FinanceModule
{
    public class FinanceViewModel
    {
        public Int64 Id { get; set; }
        public Int64 AppointmentId_Fk { get; set; }
        public String AppointmentId { get; set; }
        public String AppointmentType { get; set; }
        public String AppointmentDate { get; set; }
        public String AppointmentInstitute { get; set; }
        public String InstituteAddress { get; set; }
        public String AppointmentTranslator { get; set; }
        public String AppointmentLanguage { get; set; }
        public String Status { get; set; }
        public String Type { get; set; }
        public String Attachments { get; set; }
        public String StartOfTheTrip { get; set; }
        public String AppointmentStart { get; set; }
        public String EndOfTheAppointment { get; set; }
        public String EndOfTheTrip { get; set; }
        public Decimal TotalHours { get; set; }
        public Decimal WordCount { get; set; }
        public Decimal Rate { get; set; }
        public Decimal FlatRate { get; set; }
        public Decimal Paragraph { get; set; }
        public Decimal Postage { get; set; }
        public Decimal Hours { get; set; }
        public Decimal Discount { get; set; }
        public Decimal RideCost { get; set; }
        public Decimal DailyAllowance { get; set; }
        public Decimal Tax { get; set; }
        public Decimal TicketCost { get; set; }
        public Decimal NetPayment { get; set; }
        public Int64 CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ApprovalDate { get; set; }
        public Int64 ApprovalBy { get; set; }

        public DateTime CompletionDate { get; set; }
        public Int64 CompletionBy { get; set; }
        public String isDeleted { get; set; }
    }
}
