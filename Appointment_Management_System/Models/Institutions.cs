using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Appointment_Management_System.Models
{
    public class Institutions
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 Id { get; set; }

        [MaxLength(128)]
        [Required(AllowEmptyStrings = false)]
        public String Name { get; set; }

        [MaxLength(128)]
        [EmailAddress]
        [Required(AllowEmptyStrings = false)]
        public String Email { get; set; }

        [MaxLength(10)]
        public String Type { get; set; }

        [MaxLength(250)]
        public String Address { get; set; }

        public DateTime CreatedAt { get; set; }

        [Required(AllowEmptyStrings= false)]
        public Int64 CreatedBy { get; set; }


        [MaxLength(1)]
        public String isDeleted { get; set; }
    }
}
