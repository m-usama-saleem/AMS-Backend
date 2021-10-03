using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Appointment_Management_System.ViewModels.UserManagement
{
    public class UserViewModel
    {
        public Int64 Id { get; set; }

        [MaxLength(128)]
        [EmailAddress]
        [Required(AllowEmptyStrings = false)]
        public String Email { get; set; }

        [MaxLength(128)]
        [Required(AllowEmptyStrings = false)]
        public String Name { get; set; }

        [DataType(DataType.Password)]
        public String Password { get; set; }

        [MaxLength(10)]
        [Required(AllowEmptyStrings = false)]
        public String Status { get; set; }

        [MaxLength(10)]
        [Required(AllowEmptyStrings = false)]
        public String Type { get; set; }
        public DateTime CreatedAt { get; set; }

        [Required(AllowEmptyStrings = false)]
        public Int64 CreatedBy { get; set; }
    }
}
