using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Appointment_Management_System.ViewModels.EmailManagement
{
    public class EmailTemplateViewModel
    {
        public int EmailTemplateId { get; set; }
        public string TemplateName { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string DynamicValues { get; set; }
        public string FromName { get; set; }
        public string FromEmail { get; set; }
        public string FromPassword { get; set; }
    }
}
