using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Appointment_Management_System.Models
{
    public class Finance
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        public Int64 AppointmentId { get; set; }

        [MaxLength(25)]
        [Required(AllowEmptyStrings = false)]
        public String Status { get; set; }

        [MaxLength(20)]
        [Required(AllowEmptyStrings = false)]
        public String Type { get; set; }

        [MaxLength(500)]
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

        [Required(AllowEmptyStrings = false)]
        public Int64 CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ApprovalDate { get; set; }
        public Int64 ApprovalBy { get; set; }

        public DateTime CompletionDate { get; set; }
        public Int64 CompletionBy { get; set; }
        [MaxLength(1)]
        public String isDeleted { get; set; }
    }
}
