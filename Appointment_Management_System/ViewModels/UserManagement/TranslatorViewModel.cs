using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Appointment_Management_System.ViewModels.UserManagement
{
    public class TranslatorViewModel
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
        
        [MaxLength(10)]
        [Required(AllowEmptyStrings = false)]
        public String Language { get; set; }

        public DateTime CreatedAt { get; set; }

        [MaxLength(128)]
        [Required(AllowEmptyStrings = false)]
        public String CreatedBy { get; set; }
    }
}
