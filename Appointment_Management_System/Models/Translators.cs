using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Appointment_Management_System.Models
{
    public class Translators
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 Id { get; set; }

        [MaxLength(128)]
        [Required(AllowEmptyStrings = false)]
        public String FirstName { get; set; }

        [MaxLength(128)]
        [Required(AllowEmptyStrings = false)]
        public String LastName { get; set; }

        [MaxLength(128)]
        [EmailAddress]
        [Required(AllowEmptyStrings = false)]
        public String Email { get; set; }

        [MaxLength(128)]
        [Required(AllowEmptyStrings = false)]
        public String Language { get; set; }

        [MaxLength(25)]
        public String Country { get; set; }

        [MaxLength(10)]
        public String Gender { get; set; }

        [MaxLength(250)]
        public String Address { get; set; }

        [MaxLength(20)]
        public String PostCode { get; set; }

        [MaxLength(25)]
        public String City { get; set; }

        [MaxLength(50)]
        public String Contact { get; set; }

        [MaxLength(10)]
        public String Type { get; set; }
        public DateTime CreatedAt { get; set; }

        [Required(AllowEmptyStrings= false)]
        public Int64 CreatedBy { get; set; }


        [MaxLength(1)]
        public String isDeleted { get; set; }
    }
}
