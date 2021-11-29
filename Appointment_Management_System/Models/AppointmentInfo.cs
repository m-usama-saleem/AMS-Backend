using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Appointment_Management_System.Models
{
    public class AppointmentInfo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 Id { get; set; }

        [MaxLength(20)]
        [Required(AllowEmptyStrings = false)]
        public String AppointmentId { get; set; }

        [Required(AllowEmptyStrings = false)]
        public Int64 TranslatorId { get; set; }

        [Required(AllowEmptyStrings = false)]
        public Int64 InstitutionId { get; set; }

        [MaxLength(20)]
        [Required(AllowEmptyStrings = false)]
        public String Type { get; set; }

        [MaxLength(25)]
        [Required(AllowEmptyStrings = false)]
        public String Status { get; set; }

        [MaxLength(100)]
        [Required(AllowEmptyStrings = false)]
        public String Language { get; set; }

        [MaxLength(500)]
        public String Attachments { get; set; }

        public String RoomNumber { get; set; }
        public String AppointmentTime { get; set; }

        public DateTime EntryDate { get; set; }
        public DateTime AppointmentDate { get; set; }
        public DateTime ApprovalDate { get; set; }
        public Int64 ApprovalBy { get; set; }

        public DateTime CompletionDate { get; set; }
        public Int64 CompletionBy { get; set; }
        public System.Nullable<DateTime> DeletedDate { get; set; }
        public System.Nullable<Int64> DeletedBy { get; set; }
        public String DeletedReason { get; set; }

        public Decimal Tax { get; set; }
        public Decimal Rate { get; set; }
        public Decimal Hours { get; set; }
        public Decimal Discount { get; set; }
        public Decimal NetPayment { get; set; }

        [MaxLength(500)]
        public String Remarks { get; set; }
        [MaxLength(25)]
        public String InvoiceID { get; set; }

        [Required(AllowEmptyStrings = false)]
        public Int64 CreatedBy { get; set; }

        [MaxLength(1)]
        public String isDeleted { get; set; }
    }
}
