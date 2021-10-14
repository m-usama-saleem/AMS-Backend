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

        [MaxLength(10)]
        [Required(AllowEmptyStrings = false)]
        public String Type { get; set; }
        
        [MaxLength(128)]
        [Required(AllowEmptyStrings = false)]
        public String Language { get; set; }

        [MaxLength(128)]
        [Required(AllowEmptyStrings = false)]
        public String FirstName{ get; set; }
        public String LastName { get; set; }
        public String Contact { get; set; }
        public String Address { get; set; }
        public String City { get; set; }
        public String PostCode { get; set; }
        public String Country { get; set; }
        public String Gender { get; set; }

        public DateTime CreatedAt { get; set; }

        [Required(AllowEmptyStrings = false)]
        public Int64 CreatedBy { get; set; }
    }
}
