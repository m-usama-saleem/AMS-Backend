using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Appointment_Management_System.ViewModels.UserManagement
{
    public class InstitutionViewModel
    {
        public Int64 Id { get; set; }

        [MaxLength(128)]
        [EmailAddress]
        [Required(AllowEmptyStrings = false)]
        public String Email { get; set; }

        [MaxLength(128)]
        [Required(AllowEmptyStrings = false)]
        public String Name { get; set; }

        [MaxLength(10)]
        [Required(AllowEmptyStrings = false)]
        public String Type { get; set; }

        [MaxLength(250)]
        [Required(AllowEmptyStrings = false)]
        public String Address { get; set; }

        public DateTime CreatedAt { get; set; }

        [Required(AllowEmptyStrings = false)]
        public Int64 CreatedBy { get; set; }

    }
}
